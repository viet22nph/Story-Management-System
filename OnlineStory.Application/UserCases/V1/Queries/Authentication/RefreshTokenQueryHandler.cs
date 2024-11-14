

using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Helper;
using OnlineStory.Contract.Services.V1.Authentication;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static OnlineStory.Contract.Services.V1.Authentication.Query;
using static OnlineStory.Contract.Services.V1.Authentication.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Authentication;


public class RefreshTokenQueryHandler : IQueryHandler<RefreshTokenQuery, RefreshTokenResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICacheManager _cacheManager;
    private readonly IJwtTokenService _jwtTokenService;
    public RefreshTokenQueryHandler(UserManager<AppUser> userManager, ICacheManager cacheManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _cacheManager = cacheManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    { // check user id input
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            return Error.NotFound(code: nameof(request.UserId), description: "User id not found");
        }
        // check refresh token exists in redis
        string key = $"list-refresh-token:{request.UserId}-{request.Device}";
        string refreshToken = await _cacheManager.GetAsync(key);
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return Error.Unauthorized(code: nameof(request.RefreshToken), description: "The refresh token has expired.Please log in again");
        }
        // check refersh 
        if (refreshToken.Trim('"') != request.RefreshToken)
        {
            // remove refresh token in redis
            _cacheManager.Remove(key);
            return Error.Unauthorized(code: nameof(request.RefreshToken), description: "The refresh token is invalid or does not match the one stored. Please log in again.");
        }
        // generate jwtoken and refresh token 
        List<Claim> claims = await GetValidClaims(user);

        var generateAccessToken = _jwtTokenService.GenerateJWToken(claims);
        var generateRefreshToken = _jwtTokenService.GenerateRefreshToken();
        // overwrite refresh token in redis
        int cacheTime = (int)TimeSpan.FromDays(7).TotalMinutes;
        await _cacheManager.SetAsync(key, generateRefreshToken, cacheTime);
        Response.RefreshTokenResponse refreshTokenResponse = new Response.RefreshTokenResponse(generateAccessToken, generateRefreshToken);
        return refreshTokenResponse;
    }
    // về sau đưa vào repository
    private async Task<List<Claim>> GetValidClaims(AppUser user)
    {
        IdentityOptions _options = new IdentityOptions();
        string ip = Helper.GetIpAddress();
        var claims = new List<Claim>
            {  new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
                new Claim("ip", ip),
            };
       
        return claims;
    }
}
