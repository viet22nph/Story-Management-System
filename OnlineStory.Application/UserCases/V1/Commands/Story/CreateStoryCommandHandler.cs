

using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Extensions;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using OnlineStory.Application.Abstractions;
using OnlineStory.Domain.Abstractions.RepositoryBase;
using OnlineStory.Domain.Enums;
using static OnlineStory.Contract.Services.V1.Story.Command;

namespace OnlineStory.Application.UserCases.V1.Commands.Story;

public class CreateStoryCommandHandler : ICommandHandler<CreateStoryCommand, Success>
{
    private readonly IGenericRepository<OnlineStory.Domain.Entities.Story, Guid> _storyRepository;
    private readonly IImageStorageService _imageStorageService;
    private readonly IUnitOfWork _unitOfWork;
    public CreateStoryCommandHandler(IGenericRepository<OnlineStory.Domain.Entities.Story, Guid> storyRepository,
         IImageStorageService imageStorageService,
         IUnitOfWork unitOfWork)
    {
        _imageStorageService = imageStorageService;
        _storyRepository = storyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Success>> Handle(CreateStoryCommand request, CancellationToken cancellationToken)
    {
         var checkStoryTitle = _storyRepository.FindSingleAsync(x => x.StoryTitle == request.StoryTitle);
        if (checkStoryTitle.Result is not null)
        {
            return Error.Validation(code: nameof(Domain.Entities.Story.StoryTitle), description: "Story title is exists");
        }
        if (!Enum.TryParse<Audience>(request.Audience, true, out var audienceEnum))
        {
            // Handle invalid input (e.g., return an error response)
            return Error.Validation(code: nameof(Domain.Entities.Story.Audience), description: "Invalid audience type");
        }
        if (!Enum.TryParse<StoryStatus>(request.Status, true, out var storyStatusEnum))
        {
            // Handle invalid input (e.g., return an error response)
            return Error.Validation(code: nameof(Domain.Entities.Story.StoryStatus), description: "Invalid story status type");
        }
        string thumbnailPath = string.Empty;
        try
        {
            var imageSaveResult = await _imageStorageService.SaveImageAsync(request.Thumbnail, request.StoryTitle.ToSlug());
            if (imageSaveResult.IsError)
            {
                return Error.Validation(nameof(request.Thumbnail), imageSaveResult.Errors[0].Description);
            }
            thumbnailPath = imageSaveResult.Value; // Cập nhật đường dẫn thumbnail mới
        }
        catch (InvalidOperationException ex)
        {
            return Error.Validation(code: nameof(Domain.Entities.Story.Thumbnail), description: "File extension not allowed.");
        }

        var story = Domain.Entities.Story.Create(
           request.StoryTitle,
           request.AnotherStoryTitle,
           request.Description,
           request.Author,
           thumbnailPath,  // Use the saved thumbnail path
           request.CountryId,
           storyStatusEnum,
           audienceEnum,
           request.Slug
        );
        story.SetStoryGenresByGenreIds(request.GenresId);
        _storyRepository.Add(story);
        await _unitOfWork.SaveChangesAsync();
        return ResultType.Success;
    }
}
