

using Microsoft.AspNetCore.Http;
using OnlineStory.Contract.Abstractions.Message;

namespace OnlineStory.Contract.Services.V1.Chapter;

public class Command
{

    public record CreateChapterCommand(int ChapterNumber,
        string ChapterTitle, 
        Guid StoryId, 
        IFormFileCollection Images
        ) : ICommand<Success>;
    public record UpdateChapterCommand(
        int Id,                     
        int ChapterNumber,        
        string ChapterTitle,     
        Guid StoryId,              
        IFormFileCollection? NewImages, 
        List<int>? ImagesIdDelete   
    ) : ICommand<Success>;
}
