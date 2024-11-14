

using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using static OnlineStory.Contract.Services.V1.Comment.Command;

namespace OnlineStory.Application.UserCases.V1.Commands.Comment
{
    public  class CreateCommentChapterCommandHandler : ICommandHandler<CreateCommentChapterCommand, Success>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommentChapterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Success>> Handle(CreateCommentChapterCommand request, CancellationToken cancellationToken)
        {
            var comment = new Domain.Entities.Comment(request.Content, request.UserId, request.ChapterId, request.ParentId);
            
            try
            {
                await _unitOfWork.CommentStoryRepository.AddCommentAsync(comment);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResultType.Success;
            }
            catch (Exception ex)
            {
                return Error.Internal(description: ex.Message);
            }
        }
    }
}
