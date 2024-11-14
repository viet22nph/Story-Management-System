
using MediatR;

namespace OnlineStory.Contract.Abstractions.Message
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
    }
}
