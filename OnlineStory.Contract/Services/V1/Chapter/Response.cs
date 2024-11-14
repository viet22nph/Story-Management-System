
using static OnlineStory.Contract.Services.V1.Story.Response;

namespace OnlineStory.Contract.Services.V1.Chapter;

public class Response
{ 
    public record ChapterResponse(  
         int Id ,
         int ChapterNumber ,
         string ChapterTitle ,
         string Slug ,
         DateTimeOffset CreatedDate 
    );

    public record ChapterDetailResponse(
        int Id,
        int ChapterNumber,
        string ChapterTitle,
        string Slug,
        DateTimeOffset CreatedDate,
        string StorySlug,
        List<string> ContentUrls,
        ChapterNavigator ChapterNav
        
    );
    public record StoryWithChapterResponse(StoryResponse Story, List<ChapterResponse> Chapters);
}

public record ChapterNavigator(string? NextSlug, string? PrevSlug);