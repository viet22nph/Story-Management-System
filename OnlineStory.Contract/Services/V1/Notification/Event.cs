

using OnlineStory.Contract.Abstractions.Message;

namespace OnlineStory.Contract.Services.V1.Notification;

public class Event
{
    public record NotificationChapterAddedEvent(Guid StoryId, int ChapterNumber, string ChapterTitle): IEvent;

}
