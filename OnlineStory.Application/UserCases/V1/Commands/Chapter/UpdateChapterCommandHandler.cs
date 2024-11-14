

using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Extensions;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using static OnlineStory.Contract.Services.V1.Chapter.Command;

namespace OnlineStory.Application.UserCases.V1.Commands.Chapter;

public class UpdateChapterCommandHandler : ICommandHandler<UpdateChapterCommand, Success>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageStorageService _imageStorageService;
    public UpdateChapterCommandHandler(IUnitOfWork unitOfWork, IImageStorageService imageStorageService)
    {
        _imageStorageService = imageStorageService;
        _unitOfWork = unitOfWork;   
    }

    public async Task<Result<Success>> Handle(UpdateChapterCommand request, CancellationToken cancellationToken)
    {

        var story = await  _unitOfWork.StoryRepository.FindByIdAsync(request.StoryId);
        if(story is null)
        {
            return Error.NotFound(description: "Not found chapter");
        }
        var chapter = await _unitOfWork.ChapterRepository.FindByIdAsync(request.Id, cancellationToken, x => x.Images);
        if (chapter is null)
        {
            return Error.NotFound(description: "Not found chapter");
        }
        using var transaction = _unitOfWork.GetDbContext().Database.BeginTransaction();
        try
        {
           
            chapter.Update(request.ChapterNumber, request.ChapterTitle);

            // remove images exists ima
            var imageRemove = chapter.Images.Where(img => request.ImagesIdDelete.Contains(img.Id)).ToList();
            if (request.ImagesIdDelete is not null)
            {
                foreach (var image in imageRemove)
                {
                    chapter.RemoveImage(image);
                }
            }

            var saveImagesResult = await SaveChapterImagesAsync(request.NewImages, story.StoryTitle.ToSlug(), request.ChapterNumber, cancellationToken);
            if (saveImagesResult.IsError)
            {
                return saveImagesResult.Errors; // Return image save errors if any
            }
            var images = saveImagesResult.Value;
            chapter.AddImageRange(images);

            _unitOfWork.ChapterRepository.Update(chapter);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            // xóa image trong local
            var tasks = imageRemove.Select(async img =>
            {
                await _imageStorageService.DeleteImageAsync(img.ImageUrl);
            });
            await Task.WhenAll(tasks);
            return ResultType.Success;
        }
        catch (Exception ex) { 
            await transaction.RollbackAsync(cancellationToken);
            return Error.NotFound(description: ex.Message);
            
        }
    }
    private async Task<Result<List<ChapterImage>>> SaveChapterImagesAsync(IFormFileCollection images, string storySlug, int chapterNumber, CancellationToken cancellationToken)
    {
        var saveTasks = images.Select(file => _imageStorageService.SaveImageAsync(file, $"{storySlug}/chuong-{chapterNumber}"));
        var saveResults = await Task.WhenAll(saveTasks);

        var errors = saveResults.Where(result => result.IsError).ToList();
        var savedImagePaths = saveResults.Select(result => result.Value);
        if (errors.Any())
        {
            var deleteTasks = savedImagePaths.Select(path => _imageStorageService.DeleteImageAsync(path));
            await Task.WhenAll(deleteTasks);
            return Error.Validation("ImageSaveErrors", string.Join("; ", errors.Select(e => e.Errors[0].Description)));
        }
        return saveResults
                   .Where(result => !result.IsError)
            .Select(result => new ChapterImage(result.Value))
            .ToList();
         
    }
}
