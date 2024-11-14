

using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using static OnlineStory.Contract.Services.V1.Comment.Query;
using static OnlineStory.Contract.Services.V1.Comment.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Comment;

public class GetCommentChapterQueryHandler : IQueryHandler<GetCommentChapterQuery, Pagination<CommentResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetCommentChapterQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Pagination<CommentResponse>>> Handle(GetCommentChapterQuery request, CancellationToken cancellationToken)
    {
        int offset = (request.PageIndex - 1) * request.PageSize;
        int limit = request.PageSize;

        var comments = await _unitOfWork.CommentChapterRepository.GetCommentByParentIdAsync(request.ChapterId, request.CommentParent, offset, limit);
        var countComment = await _unitOfWork.CommentChapterRepository.CountCommentByParentIdAsync(request.ChapterId, request.CommentParent);
        var commentRespose = comments
           .Select(x => new CommentResponse(x.Id, x.ParentId, x.Content, x.Author))
           .ToList();
        return Pagination<CommentResponse>.Create(commentRespose, request.PageIndex, request.PageSize, countComment);
    }
}
