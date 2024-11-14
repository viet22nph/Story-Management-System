

using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using static OnlineStory.Contract.Services.V1.Story.Query;
using static OnlineStory.Contract.Services.V1.Story.Response;
namespace OnlineStory.Application.UserCases.V1.Queries.Story;

public class GetStoryByIdQueryHandler : IQueryHandler<GetStoryByIdQuery, StoryDetailResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStoryByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<StoryDetailResponse>> Handle(GetStoryByIdQuery request, CancellationToken cancellationToken)
    {
       var story = await _unitOfWork.StoryRepository.GetStoryByIdAsync(request.Id);
        if (story is null) {
            return Error.NotFound(description: "Not found story");
        }
        return new StoryDetailResponse(Story: story);

    }
}
