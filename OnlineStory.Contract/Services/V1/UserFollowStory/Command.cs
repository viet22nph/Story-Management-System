
using OnlineStory.Contract.Abstractions.Message;

namespace OnlineStory.Contract.Services.V1.UserFollowStory;

public class Command
{
    public record FollowStoryCommand(Guid StoryId, Guid UserId):ICommand<Success>;
}
