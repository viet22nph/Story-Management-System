

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Dtos.ChapterDtos;
using Swashbuckle.AspNetCore.Annotations;

namespace OnlineStory.Contract.Services.V1.Chapter;

public class Command
{

    public record CreateChapterCommand(int ChapterNumber,
        string ChapterTitle, 
        Guid StoryId,
        List<ImageDto> Images,
        Guid UserId
        ) : ICommand<Success>;
    public record UpdateChapterCommand(
        int Id,                     
        int ChapterNumber,        
        string ChapterTitle,     
        Guid StoryId,              
        List<ImagesUpdateDto>? Images,
        Guid UserId
    ) : ICommand<Success>;
   
}
