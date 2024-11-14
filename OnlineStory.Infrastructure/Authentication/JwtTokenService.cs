using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Infrastructure.DependencyInjection.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace OnlineStory.Infrastructure.Authentication
{
    public class JwtTokenService: IJwtTokenService
    {

        private readonly JwtOptions _jwtOptions;

        public JwtTokenService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrEmpty(_jwtOptions.SecretKey))
            {
                throw new Exception("Jwt key string is empty");
            }
        }

        public string GenerateRefreshToken()
        {
         
                var randomNumber = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    return Convert.ToBase64String(randomNumber);
                }
        }
        public string GenerateJWToken(List<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMin),
                signingCredentials: signingCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return tokenString;
        }
        public ClaimsPrincipal GetClaimsPrincipalFromExpriedToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_jwtOptions.SecretKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, // on production is true
                ValidateIssuer = false, // on production is true
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null)
            {
                throw new SecurityTokenException("Invalid token !");
            }
            return principal;
        }
    }
}
