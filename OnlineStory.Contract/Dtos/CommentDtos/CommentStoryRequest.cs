
namespace OnlineStory.Contract.Dtos.CommentDtos;

public class CommentStoryRequest
{
    public string Content { get; set; }
    public Guid StoryId { get; set; }
    public int? CommentParent { get; set; }
}
