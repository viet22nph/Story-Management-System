

using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using static OnlineStory.Contract.Services.V1.Story.Response;
using static OnlineStory.Contract.Services.V1.UserFollowStory.Response;

namespace OnlineStory.Contract.Services.V1.UserFollowStory;

public class Query
{
    public record GetStoriesUserFollowQuery(Guid UserId, int PageIndex, int PageSize): IQuery<Pagination<StoryFollowResponse>>;
}
