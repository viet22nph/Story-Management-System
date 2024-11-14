

using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using static OnlineStory.Contract.Services.V1.Story.Command;

namespace OnlineStory.Application.UserCases.V1.Commands.Story;

public class IncrementViewCountStoryCommandHandler : ICommandHandler<IncrementViewCountStoryCommand, Success>
{

    private readonly IUnitOfWork _unitOfWork;
    public IncrementViewCountStoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    /// <summary>
    /// Handler which processes the command when
    /// Increment view in story 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<Result<Success>> Handle(IncrementViewCountStoryCommand request, CancellationToken cancellationToken)
    {
        // check story
        var story = await _unitOfWork.StoryRepository.FindByIdAsync(request.StoryId, cancellationToken, x=> x.StoryViews);
        if(story is null)
        {
            return Error.NotFound(description: "Story not found");
        }
        await story.IncrementViewAsync();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ResultType.Success;

    }
}
