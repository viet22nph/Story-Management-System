

using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Extensions;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using OnlineStory.Domain.Abstractions.RepositoryBase;
using OnlineStory.Domain.Enums;
using static OnlineStory.Contract.Services.V1.Story.Command;

namespace OnlineStory.Application.UserCases.V1.Commands.Story;

public class UpdateStoryCommandHandler : ICommandHandler<UpdateStoryCommand, Success>
{
    private readonly IGenericRepository<OnlineStory.Domain.Entities.Story, Guid> _storyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageStorageService _imageStorageService;
    public UpdateStoryCommandHandler(IGenericRepository<OnlineStory.Domain.Entities.Story, Guid> storyRepository,
        IUnitOfWork unitOfWork, IImageStorageService imageStorageService)
    {
        _storyRepository = storyRepository;
        _unitOfWork = unitOfWork;
        _imageStorageService = imageStorageService;
    }
    public async Task<Result<Success>> Handle(UpdateStoryCommand request, CancellationToken cancellationToken)
    {
        var story = await _storyRepository.FindByIdAsync(request.StoryId);
        if (story == null)
        {
            return Error.NotFound("Story not found.");
        }
        // Validate Audience
        if (!Enum.TryParse<Audience>(request.Audience, true, out var audienceEnum))
        {
            return Error.Validation(code: nameof(Domain.Entities.Story.Audience), description: "Invalid audience type");
        }

        // Validate Story Status
        if (!Enum.TryParse<StoryStatus>(request.Status, true, out var storyStatusEnum))
        {
            return Error.Validation(code: nameof(Domain.Entities.Story.StoryStatus), description: "Invalid story status type");
        }

        // Process the new thumbnail if provided
       // Lưu ảnh mới (nếu có) và lưu lại đường dẫn tạm thời
        string newThumbnailPath = story.Thumbnail;
        string thumbnailUrlBeforeUpdate = story.Thumbnail;
        bool isNewThumbnailSaved = false;

        if (request.Thumbnail != null)
        {
            var imageSaveResult = await _imageStorageService.SaveImageAsync(request.Thumbnail, request.StoryTitle.ToSlug());
            if (imageSaveResult.IsError)
                return Error.Validation(nameof(request.Thumbnail), imageSaveResult.Errors[0].Description);

            newThumbnailPath = imageSaveResult.Value;
            isNewThumbnailSaved = true;
        }
        var strategy = _unitOfWork.GetDbContext().Database.CreateExecutionStrategy();
        try
        {
            return await strategy.ExecuteAsync(async () =>
            {
                // Cập nhật thông tin story
                story.SetStoryGenresByGenreIds(request.GenresId);
                story.Update(
                    storyTitle: request.StoryTitle,
                    anotherStoryTitle: request.AnotherStoryTitle,
                    description: request.Description,
                    author: request.Author,
                    thumbnail: newThumbnailPath,
                    countryId: request.CountryId,
                    storyStatus: storyStatusEnum,
                    audience: audienceEnum
                );

                // Cập nhật repository và lưu thay đổi
                _storyRepository.Update(story);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Nếu thành công và ảnh mới đã lưu, xóa ảnh cũ
                if (isNewThumbnailSaved && thumbnailUrlBeforeUpdate != newThumbnailPath)
                {
                    await _imageStorageService.DeleteImageAsync(thumbnailUrlBeforeUpdate);
                }

                return ResultType.Success;
            });
        }
        catch (Exception ex)
        {
            // Nếu xảy ra lỗi, xóa ảnh mới nếu đã lưu
            if (isNewThumbnailSaved)
            {
                await _imageStorageService.DeleteImageAsync(newThumbnailPath);
            }

            return Error.Validation("UpdateFailed", description: $"Update failed: {ex.Message}");
        }
    }
}
