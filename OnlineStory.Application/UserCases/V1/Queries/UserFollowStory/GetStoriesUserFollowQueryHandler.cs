using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using System.Linq.Expressions;
using static OnlineStory.Contract.Services.V1.Story.Response;
using static OnlineStory.Contract.Services.V1.UserFollowStory.Query;
using static OnlineStory.Contract.Services.V1.UserFollowStory.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.UserFollowStory;

public class GetStoriesUserFollowQueryHandler : IQueryHandler<GetStoriesUserFollowQuery, Pagination<StoryFollowResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;
    public GetStoriesUserFollowQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _contextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
        _userManager = userManager; 
    }

    public async Task<Result<Pagination<StoryFollowResponse>>> Handle(GetStoriesUserFollowQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }
        var query = _unitOfWork.UserFollowStoryRepository.FindAll(x=> x.UserId == request.UserId).AsNoTracking();
        var httpRequest = _contextAccessor.HttpContext.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";
        var stories = await query.Include(x => x.Story)
            .OrderBy(x => x.FollowAt)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new StoryFollowResponse(x.Story.Id, x.Story.StoryTitle, x.Story.AnotherStoryTitle, $"{baseUrl}/{x.Story.Thumbnail}", x.Story.Slug, x.FollowAt))
            .ToListAsync(cancellationToken);
        var count = await query.CountAsync(cancellationToken);
        return Pagination<StoryFollowResponse>.Create(stories, request.PageIndex, request.PageSize, count);
    }
}
