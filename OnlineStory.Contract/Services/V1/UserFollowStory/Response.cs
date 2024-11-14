
namespace OnlineStory.Contract.Services.V1.UserFollowStory;

public class Response
{
    public record StoryFollowResponse(Guid Id,
        string Title,
        string? SubTitle,
        string Thumbnail,
        string Slug,
        DateTimeOffset FollowAt);
}
