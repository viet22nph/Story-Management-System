

using OnlineStory.Contract.Abstractions.Message;

namespace OnlineStory.Contract.Services.V1.ReadingHistory;

public class Command
{
    public record MarkChapterAsReadCommand(Guid UserId, Guid StoryId, int ChapterId):ICommand<Success>;
}
