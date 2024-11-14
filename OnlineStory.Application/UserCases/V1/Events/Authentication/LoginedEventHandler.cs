
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Constants;
using static OnlineStory.Contract.Services.V1.Authentication.Event;

namespace OnlineStory.Application.UserCases.V1.Events.Authentication;

public class LoginedEventHandler : IEventHandler<LoginedEvent>
{
    private readonly ICacheManager _cacheManager;
    public LoginedEventHandler(ICacheManager cacheManager)
    {
        _cacheManager = cacheManager;
    }
    public async Task Handle(LoginedEvent notification, CancellationToken cancellationToken)
    {
        string key = $"{RedisKey.LIST_REFRESH_KEY}{notification.UserId}-{notification.Device}";
        int cacheTime = (int)TimeSpan.FromDays(7).TotalMinutes;
        await _cacheManager.SetAsync(key, notification.RefreshToken, cacheTime);
    }
}
