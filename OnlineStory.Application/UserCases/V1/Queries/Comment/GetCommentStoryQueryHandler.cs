

using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Domain.Enums;
using static OnlineStory.Contract.Services.V1.Comment.Query;
using static OnlineStory.Contract.Services.V1.Comment.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Comment;

public class GetCommentStoryQueryHandler : IQueryHandler<GetCommentStoryQuery, Pagination<CommentResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetCommentStoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<Pagination<CommentResponse>>> Handle(GetCommentStoryQuery request, CancellationToken cancellationToken)
    {

        int offset = (request.PageIndex - 1) * request.PageSize;
        int limit = request.PageSize;

        var comments = await _unitOfWork.CommentStoryRepository.GetCommentByParentIdAsync( request.StoryId ,request.CommentParent, offset, limit);
        var countComment = await _unitOfWork.CommentStoryRepository.CountCommentByParentIdAsync(request.StoryId, request.CommentParent);
        var commentRespose = comments
           .Select(x => new CommentResponse(x.Id, x.ParentId, x.Content, x.Author))
           .ToList();
        return Pagination<CommentResponse>.Create(commentRespose, request.PageIndex, request.PageSize, countComment);
    }
}
