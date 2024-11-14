
namespace OnlineStory.Contract.Dtos.CommentDtos;

public class CommentDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Content { get; set; }
    public int Left {  get; set; }
    public int Right { get; set; }
    public CommentUserDto Author { get; set; }
}
public class CommentUserDto
{
    public Guid UserId { get; set; }
    public string DisplayName{  get; set; }
    public string? AvatarUrl {  get; set; }
}
