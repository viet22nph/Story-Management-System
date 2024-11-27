

using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Dtos.ChapterDtos;
using OnlineStory.Contract.Extensions;
using OnlineStory.Contract.Share;
using OnlineStory.Contract.Share.Errors;
using OnlineStory.Domain.Entities.Identity;
using static OnlineStory.Contract.Services.V1.Chapter.Command;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineStory.Application.UserCases.V1.Commands.Chapter;

public class UpdateChapterCommandHandler : ICommandHandler<UpdateChapterCommand, Success>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageStorageService _imageStorageService;
    private readonly int _batchSize = 20;
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
            return Error.NotFound(description: "Not found story");
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
            var imagesExists = request.Images.Where(x=>x.ImageChapterId!= null).Select(x=> x.ImageChapterId).ToList();
            // remove images exists ima
            var imageRemove = chapter.Images.Where(img => !imagesExists.Contains(img.Id)).ToList();
          
            foreach (var image in imageRemove)
            {
                chapter.RemoveImage(image);
            }

            var newImages = request.Images.Where(x => x.File != null).Select(x => new ImageDto(x.File, x.Order)).ToList();
           if(newImages is not null)
           {
                var saveImagesResult = await SaveChapterImagesInBatchesAsync(newImages, story.StoryTitle.ToSlug(), request.ChapterNumber, _batchSize, cancellationToken);
                if (saveImagesResult.IsError)
                {
                    return saveImagesResult.Errors; // Return image save errors if any
                }
                var images = saveImagesResult.Value;
                chapter.AddImageRange(images);

            }    

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
    private async Task<Result<List<ChapterImage>>> SaveChapterImagesInBatchesAsync(
     List<ImageDto> images,
     string storySlug,
     int chapterNumber,
     int batchSize,
     CancellationToken cancellationToken)
    {
        var batches = images
        .Select((value, index) => new { value, index })
        .GroupBy(x => x.index / batchSize)
        .Select(g => g.Select(x => x.value).ToList());
        var chapterImages = new List<ChapterImage>();
        var errors = new List<string>();
        var savedImagePaths = new List<string>();

        foreach (var batch in batches)
        {
            var tasks = batch.Select(async image =>
            {
                var imageSaveResult = await _imageStorageService.SaveImageAsync(image.File, $"{storySlug}/chuong-{chapterNumber}");

                if (imageSaveResult.IsError)
                {
                    errors.Add($"Failed to save image {image.File.FileName}: {imageSaveResult.Errors[0].Description}");
                }
                else
                {
                    savedImagePaths.Add(imageSaveResult.Value);
                    chapterImages.Add(new ChapterImage(imageSaveResult.Value, image.Order));
                }
            });

            await Task.WhenAll(tasks);

            // Nếu batch gặp lỗi, xóa ảnh đã lưu
            if (errors.Any())
            {
                var deleteTasks = savedImagePaths.Select(path => _imageStorageService.DeleteImageAsync(path));
                await Task.WhenAll(deleteTasks);

                return Error.Validation("ImageSaveErrors", string.Join("; ", errors));
            }
        }

        return chapterImages;
    }
}
