

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Services.V1.Chapter;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using OnlineStory.Domain.Entities;
using static OnlineStory.Contract.Services.V1.Chapter.Query;
using static OnlineStory.Contract.Services.V1.Chapter.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Chapter;

public class GetChapterBySlugQueryHandler : IQueryHandler<GetChapterBySlugQuery, ChapterDetailResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetChapterBySlugQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<Result<ChapterDetailResponse>> Handle(GetChapterBySlugQuery request, CancellationToken cancellationToken)
    {
        // check story
        var story = await _unitOfWork.StoryRepository.FindSingleAsync(x=>  x.Slug == request.StorySlug);
        if(story is null)
        {
            return Error.NotFound(description: "Story not found");
        }
        // get chapter
        var chapters = await _unitOfWork.ChapterRepository
            .FindAll(x => x.StoryId == story.Id,y=>y.Images)
            .ToListAsync();
        var chapter = chapters.FirstOrDefault(x=> x.Slug == request.ChapterSlug && x.StoryId == story.Id);
        if(chapter is null)
        {
            //error
            return Error.NotFound(description: "Chapter not found in list chapter");
        }
        int currentChapterIndex = chapters.IndexOf(chapter);
        // url next chapter if chapter current is the last chapter, return null
        var nextSlug = currentChapterIndex + 1 >= chapters.Count ? null : chapters[currentChapterIndex + 1]?.Slug;
        var previousSlug = currentChapterIndex - 1 < 0 ? null : chapters[currentChapterIndex - 1]?.Slug;
        var httpRequest = _httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";
        var nav = new ChapterNavigator(nextSlug, previousSlug);
        var chapterResponse = new ChapterDetailResponse(chapter.Id, chapter.ChapterNumber, chapter.ChapterTitle, chapter.Slug, chapter.CreatedDate, story.Slug, chapter.Images.OrderBy(x => x.Id).Select(x => $"{baseUrl}/{x.ImageUrl}").ToList(), nav);
        return chapterResponse;
    }
}
