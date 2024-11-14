
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using static OnlineStory.Contract.Services.V1.Chapter.Query;
using static OnlineStory.Contract.Services.V1.Chapter.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Chapter;

public class GetChapterListQueryHandler : IQueryHandler<GetChapterListQuery, List<ChapterResponse>>
{

    private readonly IUnitOfWork _unitOfWork;
    public GetChapterListQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<ChapterResponse>>> Handle(GetChapterListQuery request, CancellationToken cancellationToken)
    {
        var story = await _unitOfWork.StoryRepository.FindSingleAsync(x => x.Slug == request.StorySlug);
        if (story is null)
        {
            return Error.NotFound(description: "Not found story");
        }
        var chapters = await _unitOfWork.ChapterRepository.FindAll(x => x.StoryId == story.Id)
            .OrderBy(x => x.CreatedDate)
            .Select(x => new ChapterResponse(x.Id, x.ChapterNumber, x.ChapterTitle, x.Slug, x.CreatedDate))
            .ToListAsync(cancellationToken);
        return chapters;

    }
}
