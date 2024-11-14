

using OnlineStory.Contract.Abstractions.Message;
using static OnlineStory.Contract.Services.V1.Authentication.Response;

namespace OnlineStory.Contract.Services.V1.Authentication;

public static class Query
{

    public record LoginQuery(string UserNameOrEmail, string Password, string Device) : IQuery<LoginResponse>;

    public record RefreshTokenQuery(Guid UserId, string RefreshToken, string Device) : IQuery<RefreshTokenResponse>;
}
