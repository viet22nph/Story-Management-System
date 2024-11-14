
namespace OnlineStory.Contract.Dtos.ReadingHistory;

public class MarkChapterAsReadRequest
{
    public Guid StoryId { get; set; }
    public int ChapterId {  get; set; }
}
