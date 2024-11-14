
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions.Repository;

namespace OnlineStory.Application.Abstractions;

public interface IUnitOfWork
{
    IStoryRepository StoryRepository { get; }
    ICommentChapterRepository CommentChapterRepository { get; }
    ICommentStoryRepository CommentStoryRepository { get; }
    IChapterRepository ChapterRepository { get; }
    IReadingHistoryRepository ReadingHistoryRepository { get; }
    IGenreRepository GenreRepository { get; }
    IUserFollowStoryRepository UserFollowStoryRepository { get; }
    INotificationRepository NotificationRepository { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    DbContext GetDbContext();
}
