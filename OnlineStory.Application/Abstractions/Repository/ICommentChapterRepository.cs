
using OnlineStory.Contract.Dtos.CommentDtos;
using OnlineStory.Domain.Abstractions.RepositoryBase;
using OnlineStory.Domain.Entities;

namespace OnlineStory.Application.Abstractions.Repository;

public interface ICommentChapterRepository: IGenericRepository<Comment, int>
{
    /// <summary>
    /// Adds a comment to the database, adjusting the boundaries based on parent-child relationships.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task AddCommentAsync(Comment comment);


    /// <summary>
    /// Retrieves comments based on parent comment ID with pagination support.
    /// </summary>
    /// <param name="chapterId">The ID of the chapter.</param>
    /// <param name="parentId">The ID of the parent comment.</param>
    /// <param name="offset">The offset for pagination.</param>
    /// <param name="limit">The limit for pagination.</param>
    /// <returns>A Task containing a list of CommentDto objects.</returns>
    Task<List<CommentDto>> GetCommentByParentIdAsync(int chapterId, int? parentId = null, int offset = 0, int limit = 50);



    /// <summary>
    /// Counts the number of comments under a specific parent comment.
    /// </summary>
    /// <param name="chapterId">The chapter ID.</param>
    /// <param name="parentId">The parent comment ID.</param>
    /// <returns>A Task containing the count of comments.</returns>
    Task<int> CountCommentByParentIdAsync(int chapterId, int? parentId = null);

    /// <summary>
    /// Removes a comment and its child comments, updating boundaries for remaining comments.
    /// </summary>
    /// <param name="commentId">The ID of the comment to remove.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task RemoveCommentAsync(int commentId);
}
