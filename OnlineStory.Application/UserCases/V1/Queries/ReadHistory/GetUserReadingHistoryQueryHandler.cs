

using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using static OnlineStory.Contract.Services.V1.ReadingHistory.Query;
using static OnlineStory.Contract.Services.V1.ReadingHistory.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.ReadHistory;

public class GetUserReadingHistoryQueryHandler : IQueryHandler<GetUserRedingHistoryQuery, Pagination<ReadingStoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetUserReadingHistoryQueryHandler(IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<Result<Pagination<ReadingStoryResponse>>> Handle(GetUserRedingHistoryQuery request, CancellationToken cancellationToken)
    {
        // check user

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if(user is null)
        {
            return Error.Validation(description: "User not found");
        }

        var query =  _unitOfWork.ReadingHistoryRepository.FindAll(x => x.UserId == request.UserId, x => x.Story);
        var httpRequest = _httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";
        var histories = await query
            .AsNoTracking()
            .GroupBy(x=> x.StoryId)
            .Select(x=> new ReadingStoryResponse(x.Key,
            x.FirstOrDefault().Story.StoryTitle,
            x.FirstOrDefault().Story.AnotherStoryTitle,
            $"{baseUrl}/{x.FirstOrDefault().Story.Thumbnail}",
            x.FirstOrDefault().Story.Slug,
            x.OrderByDescending(x => x.LastReadAt)
                                        .FirstOrDefault().LastReadAt))
            .Skip((request.PageIndex-1)* request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
        var count = await query.GroupBy(x=> x.StoryId).CountAsync();
        return Pagination<ReadingStoryResponse>.Create(histories, request.PageIndex, request.PageSize, count);
    }
}
