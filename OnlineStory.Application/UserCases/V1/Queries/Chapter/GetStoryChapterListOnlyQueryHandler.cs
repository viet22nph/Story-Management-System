
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using static OnlineStory.Contract.Services.V1.Chapter.Query;
using static OnlineStory.Contract.Services.V1.Chapter.Response;
using static OnlineStory.Contract.Services.V1.Story.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Chapter;

public class GetStoryChapterListOnlyQueryHandler : IQueryHandler<GetStoryChapterListOnlyQuery, StoryWithChapterResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetStoryChapterListOnlyQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<Result<StoryWithChapterResponse>> Handle(GetStoryChapterListOnlyQuery request, CancellationToken cancellationToken)
    {
        var story = await _unitOfWork.StoryRepository.GetStoryBySlugAsync(request.StorySlug);
        if (story is null) {
            return Error.NotFound(description: "Not found story");
        }

        // get list chapter
        var chapters = await _unitOfWork.ChapterRepository
            .FindAll(x=> x.StoryId == story.Id)
            .Select(x=> new ChapterResponse(x.Id, x.ChapterNumber, x.ChapterTitle, x.Slug, x.CreatedDate))
            .ToListAsync();
        var httpRequest = _httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

        var storyResponse = new StoryResponse(story.Id,
            story.StoryTitle,
            story.AnotherStoryTitle,
            story.Description,
            story.Author,
            $"{baseUrl}/{story.Thumbnail}",
            story.Slug,
            story.StoryStatus,
            story.Genres.Select(x => x.Name).ToList(),
            story.Country.Name,
            false,
            story.CreatedDate,
            story.ModifiedDate);
        var dataResponse = new StoryWithChapterResponse(storyResponse, chapters);
        return dataResponse;

    }
}
