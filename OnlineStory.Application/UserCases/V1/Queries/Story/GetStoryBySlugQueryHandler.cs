

using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using static OnlineStory.Contract.Services.V1.Story.Query;
using static OnlineStory.Contract.Services.V1.Story.Response;

namespace OnlineStory.Application.UserCases.V1.Queries.Story;

public class GetStoryBySlugQueryHandler : IQueryHandler<GetStoryBySlugQuery, StoryDetailResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStoryBySlugQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<StoryDetailResponse>> Handle(GetStoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var story = await _unitOfWork.StoryRepository.GetStoryBySlugAsync(request.Slug);
        if (story is null)
        {
            return Error.NotFound(description: "Not found story");
        }
        return new StoryDetailResponse(Story: story);

    }
}
