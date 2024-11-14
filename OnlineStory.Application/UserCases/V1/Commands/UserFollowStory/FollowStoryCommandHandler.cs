
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using OnlineStory.Domain.Entities;
using static OnlineStory.Contract.Services.V1.UserFollowStory.Command;

namespace OnlineStory.Application.UserCases.V1.Commands.UserFollowStory;

public class FollowStoryCommandHandler : ICommandHandler<FollowStoryCommand, Success>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    public FollowStoryCommandHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }


    public async Task<Result<Success>> Handle(FollowStoryCommand request, CancellationToken cancellationToken)
    {
        var story = await _unitOfWork.StoryRepository.FindSingleAsync(x => x.Id == request.StoryId);
        if(story is null)
        {
            return Error.NotFound(description: "Story not found");
        }
        var user =await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        //add follow
        var userFollow = await _unitOfWork.UserFollowStoryRepository.FindSingleAsync(x=> x.UserId == user.Id && x.StoryId == story.Id);
        if(userFollow is not null)
        {
            return Error.NotFound(description: "Already following this story.");
        }
        userFollow = new UserStoryTracking( user.Id, story.Id);
        _unitOfWork.UserFollowStoryRepository.Add(userFollow);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ResultType.Success;

    } 
}
