
using System.Security.Claims;

namespace OnlineStory.Application.Abstractions.Services;

public interface IJwtTokenService
{
    string GenerateRefreshToken();
    string GenerateJWToken(List<Claim> claims);
    ClaimsPrincipal GetClaimsPrincipalFromExpriedToken(string token);
}
