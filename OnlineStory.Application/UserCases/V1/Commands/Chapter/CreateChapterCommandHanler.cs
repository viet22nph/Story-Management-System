using Domain.Entities;
using Microsoft.AspNetCore.Http;
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using OnlineStory.Domain.Abstractions.RepositoryBase;
using OnlineStory.Domain.Entities;
using static OnlineStory.Contract.Services.V1.Chapter.Command;
using OnlineStory.Contract.Share.Errors;
using OnlineStory.Contract.Extensions;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using MediatR;
using static OnlineStory.Contract.Services.V1.Notification.Event;

public class CreateChapterCommandHandler : ICommandHandler<CreateChapterCommand, Success>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageStorageService _imageStorageService;
    private readonly IPublisher _publisher;
    public CreateChapterCommandHandler(
        IUnitOfWork unitOfWork,
        IImageStorageService imageStorageService,
        IPublisher publisher
        
        )
    {
        _unitOfWork = unitOfWork;
        _imageStorageService = imageStorageService;
        _publisher = publisher;
    }

    public async Task<Result<Success>> Handle(CreateChapterCommand request, CancellationToken cancellationToken)
    {
        var checkChapterNumberExists = await _unitOfWork.ChapterRepository
            .FindSingleAsync(x => (x.ChapterNumber == request.ChapterNumber && x.StoryId == request.StoryId) ||  (x.ChapterTitle == request.ChapterTitle && x.StoryId == request.StoryId));
        if (checkChapterNumberExists is not null)
        {
            return Error.Validation(nameof(Chapter.ChapterNumber), "Chapter with the same number or title already exists for this story.");
        }
        var story = await _unitOfWork.StoryRepository.FindByIdAsync(request.StoryId, cancellationToken);
     

        if (story is null)
        {
            return Error.Validation(nameof(Chapter.StoryId), "Story id not found.");
        }
        var images = new List<ChapterImage>();

        var saveImagesResult = await SaveChapterImagesAsync(request.Images, story.StoryTitle.ToSlug(), request.ChapterNumber, images, cancellationToken);
        if (saveImagesResult.IsError)
        {
            return saveImagesResult; // Return image save errors if any
        }

        try
        {
            var chapter = new Chapter(request.ChapterNumber, request.ChapterTitle, story);
            chapter.AddImageRange(images);

            _unitOfWork.ChapterRepository.Add(chapter);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var notificationEvent = new NotificationChapterAddedEvent(story.Id, chapter.ChapterNumber, chapter.ChapterTitle);
            await _publisher.Publish(notificationEvent, cancellationToken);
            return ResultType.Success;
        }
        catch (Exception ex)
        {
            return Error.Internal(ex.Message);
        }
    }

    private async Task<Result<Success>> SaveChapterImagesAsync(IFormFileCollection images, string storySlug, int chapterNumber, List<ChapterImage> chapterImages, CancellationToken cancellationToken)
    {
        var errors = new List<string>();
        var savedImagePaths = new List<string>();

        var tasks = images.Select(async file =>
        {
            var imageSaveResult = await _imageStorageService.SaveImageAsync(file, $"{storySlug}/chuong-{chapterNumber}");

            if (imageSaveResult.IsError)
            {
                errors.Add($"Failed to save image {file.FileName}: {imageSaveResult.Errors[0].Description}");
            }
            else
            {
                savedImagePaths.Add(imageSaveResult.Value);
                chapterImages.Add(new ChapterImage(imageSaveResult.Value));
            }
        });

        await Task.WhenAll(tasks);

        if (errors.Any())
        {
            var deleteTasks = savedImagePaths.Select(path => _imageStorageService.DeleteImageAsync(path));
            await Task.WhenAll(deleteTasks);

            return Error.Validation("ImageSaveErrors", string.Join("; ", errors));
        }

        return ResultType.Success;
    }
}
