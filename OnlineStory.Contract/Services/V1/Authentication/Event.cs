
using OnlineStory.Contract.Abstractions.Message;

namespace OnlineStory.Contract.Services.V1.Authentication;

public class Event
{
    public record LoginedEvent(Guid UserId, string RefreshToken, string Device) : IEvent;
}
