

using OnlineStory.Contract.Dtos;

namespace OnlineStory.Contract.Services.V1.Authentication;

public class Response
{
    public record LoginResponse(UserLoginDto User, string AccessToken, string RefreshToken );
    public record RefreshTokenResponse(string AccessToken, string RefreshToken);
}
