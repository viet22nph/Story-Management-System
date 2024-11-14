

using Domain.Entities.Identity;
using OnlineStory.Contract.Share.Errors;
using OnlineStory.Domain.Abstractions;
using OnlineStory.Domain.Abstractions.Entities;
using OnlineStory.Domain.Enums;

namespace OnlineStory.Domain.Entities;

public class Comment : EntityBase<int>, IDateTracking
{
    public Guid UserId { get; set; }
    public CommentType Type { get; set; }
    public Guid? StoryId { get; set; }
    public int? ChapterId { get; set; }
    public string Content { get; set; }
    public int? ParentCommentId { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }
    //public bool? IsDeleted { get; set; } = false;
    public virtual AppUser User { get; set; }
    public virtual Chapter? Chapter { get; set; }
    public virtual Story? Story { get; set; }
    public virtual Comment? ParentComment { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }

    private readonly List<Comment> _replies;
    public IReadOnlyCollection<Comment> Replies => _replies.AsReadOnly();
    private Comment()
    {
        _replies = new List<Comment>();
    }

    // Constructor for creating a new comment related to a story
    public Comment(string content, Guid userId, Guid storyId, int? parentComment = null): this()
    {
        Content = !string.IsNullOrWhiteSpace(content)?content: throw new ArgumentNullException(nameof(Content));
        UserId = userId == Guid.Empty ? throw new ArgumentNullException(nameof(UserId)): userId;
        Type = CommentType.Story; // Set the type to Story
        StoryId = storyId;
        CreatedDate = DateTimeOffset.UtcNow;
        ParentCommentId = parentComment;
    }
    public Comment(string content, Guid userId, int chapterId, int? parentComment = null) : this()
    {
        Content = !string.IsNullOrWhiteSpace(content) ? content : throw new ArgumentNullException(nameof(Content));
        UserId = userId == Guid.Empty ? throw new ArgumentNullException(nameof(UserId)) : userId;
        Type = CommentType.Story; // Set the type to Story
        ChapterId = chapterId;
        CreatedDate = DateTimeOffset.UtcNow;
        ParentCommentId = parentComment;
    }
    // Method to update the comment content
    public void UpdateContent(string newContent)
    {
        Content = newContent;
        ModifiedDate = DateTimeOffset.UtcNow;
    }
}
