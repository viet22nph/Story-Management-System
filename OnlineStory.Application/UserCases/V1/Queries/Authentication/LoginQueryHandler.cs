using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Dtos;
using OnlineStory.Contract.Helper;
using OnlineStory.Contract.Services.V1.Authentication;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static OnlineStory.Contract.Services.V1.Authentication.Query;
using static OnlineStory.Contract.Services.V1.Authentication.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Authentication;

public class LoginQueryHandler : IQueryHandler<LoginQuery, LoginResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    private readonly IPublisher _publisher;
    public LoginQueryHandler(UserManager<AppUser> userManager, IJwtTokenService jwtTokenService, IPublisher publisher)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _publisher = publisher;
    }


    public async Task<Result<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        // check user
        AppUser user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
        if (user == null)
        {
            user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user == null)
            {
                return Error.Validation(code: nameof(request.UserNameOrEmail), description: $"You are not registered with '{request.UserNameOrEmail}'.");
            }
        }


        bool signInResult = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!signInResult)
        {
            return Error.Validation(code: nameof(request.UserNameOrEmail), description: $"Invalid credentials for '{request.UserNameOrEmail}'.");
        }
        List<Claim> claims = await GetValidClaims(user);
        string generateAccessToken = _jwtTokenService.GenerateJWToken(claims);
        string generateRefreshToken = _jwtTokenService.GenerateRefreshToken();
        var userDto = new UserLoginDto
        {
            Id = user.Id,
            Avatar = user.Avatar,
            DisplayName = user.DisplayName,
            Email = user.Email,
        };
        var loginResponse = new LoginResponse(userDto, generateAccessToken, generateRefreshToken);
        // call event save refresh token 
        await _publisher.Publish(new Event.LoginedEvent(user.Id, generateRefreshToken, request.Device));
        return loginResponse;
    }
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
