

using OnlineStory.Contract.Dtos.CommentDtos;
using OnlineStory.Domain.Abstractions.RepositoryBase;
using OnlineStory.Domain.Entities;

namespace OnlineStory.Application.Abstractions.Repository;

public interface ICommentStoryRepository : IGenericRepository<Comment, int>
{
    /// <summary>
    /// Adds a comment to the database, adjusting the boundaries based on parent-child relationships.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task AddCommentAsync(Comment comment);

    /// <summary>
    /// Retrieves a paginated list of comments based on the parent comment ID.
    /// </summary>
    /// <param name="storyId">The unique identifier for the story to which the comments belong.</param>
    /// <param name="parentId">The ID of the parent comment. Null if retrieving root comments.</param>
    /// <param name="offset">The offset for pagination, indicating the starting point of records.</param>
    /// <param name="limit">The maximum number of comments to retrieve.</param>
    /// <returns>A list of <see cref="CommentDto"/> containing comment information.</returns>
    /// <exception cref="Exception">Thrown when the specified parent comment is not found.</exception>
    Task<List<CommentDto>> GetCommentByParentIdAsync(Guid storyId, int? parentId = null, int offset = 0, int limit = 50);

    /// <summary>
    /// Counts the total number of comments by parent ID and story ID.
    /// </summary>
    /// <param name="storyId">The unique identifier for the story to which the comments belong.</param>
    /// <param name="parentId">The ID of the parent comment. Null if counting root comments.</param>
    /// <returns>The total count of comments.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the story ID is empty.</exception>
    Task<int> CountCommentByParentIdAsync(Guid storyId, int? parentId = null);

    /// <summary>
    /// Removes a comment and its nested replies from the database, adjusting left and right boundary values accordingly.
    /// </summary>
    /// <param name="commentId">The ID of the comment to be removed.</param>
    /// <exception cref="ArgumentException">Thrown when the specified comment is not found.</exception>
    /// <exception cref="Exception">Thrown if an error occurs during transaction execution.</exception>
    Task RemoveCommentAsync(int commentId);
}
