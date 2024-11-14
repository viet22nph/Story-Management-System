
namespace OnlineStory.Contract.Dtos.CommentDtos
{
    public class CommentChapterRequest
    {
        public string Content {  get; set; }
        public int ChapterId { get; set; }
        public int? CommentParent { get; set; }
    }
}
