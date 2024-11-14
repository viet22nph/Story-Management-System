
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using static OnlineStory.Contract.Services.V1.ReadingHistory.Command;

namespace OnlineStory.Application.UserCases.V1.Commands.RedingHistory;

public class MarkChapterAsReadCommandHandler : ICommandHandler<MarkChapterAsReadCommand, Success>
{
    private readonly IUnitOfWork _unitOfWork;


    public MarkChapterAsReadCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<Success>> Handle(MarkChapterAsReadCommand request, CancellationToken cancellationToken)
    {
        var alreadyRead = await _unitOfWork.ReadingHistoryRepository.FindSingleAsync(x=> x.UserId == request.UserId && x.StoryId ==  request.StoryId && x.ChapterId == request.ChapterId);
        if(alreadyRead is not null)
        {
            // Update last time access in chapter
            alreadyRead.Update();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return ResultType.Success;

        }
        var history = new Domain.Entities.ReadingHistory(request.StoryId, request.ChapterId, request.UserId);
        _unitOfWork.ReadingHistoryRepository.Add(history);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ResultType.Success;

    }
}
