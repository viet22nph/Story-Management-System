
using MediatR;
using OnlineStory.Contract.Abstractions.Message;


namespace OnlineStory.Contract.Services.V1.Comment;

public class Command
{
    public record CreateCommentStoryCommand(string Content, Guid UserId, Guid StoryId, int? ParentId) : ICommand<Success>;

    public record CreateCommentChapterCommand(string Content, Guid UserId, int ChapterId, int? ParentId): ICommand<Success>;
    public record RemoveCommentChapterCommand(int CommentId, int ChapterId): ICommand<Success>;
    public record RemoveCommentStoryCommand(int CommentId, Guid StoryId) : ICommand<Success>;

}
