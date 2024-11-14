using OnlineStory.Contract.Dtos.CommentDtos;

namespace OnlineStory.Contract.Services.V1.Comment
{
    public class Response
    {
        public record CommentResponse(int Id, int? ParentId, string Content, CommentUserDto Author);
    }
}
