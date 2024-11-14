

using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using static OnlineStory.Contract.Services.V1.Comment.Response;

namespace OnlineStory.Contract.Services.V1.Comment;

public class Query
{
    public record GetCommentStoryQuery(Guid StoryId, int? CommentParent, int PageIndex =1, int PageSize =20):  IQuery<Pagination<CommentResponse>>;
    public record GetCommentChapterQuery(int ChapterId, int? CommentParent, int PageIndex=1, int PageSize =20):  IQuery<Pagination<CommentResponse>>;
}
