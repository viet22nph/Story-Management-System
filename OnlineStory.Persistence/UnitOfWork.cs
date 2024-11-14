

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Application.Abstractions.Repository;
using OnlineStory.Persistence.ApplicationDbContext;
using OnlineStory.Persistence.Repositories;

namespace OnlineStory.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    private IStoryRepository _storyRepository;
    public IStoryRepository StoryRepository
    {
        get
        {
            if (_storyRepository == null)
            {
                _storyRepository = new StoryRepository(_context);
            }
            return _storyRepository;
        }
    }
    private ICommentChapterRepository _commentChapterRepository;
    public ICommentChapterRepository CommentChapterRepository
    {
        get
        {
            if (_commentChapterRepository == null)
            {
                _commentChapterRepository = new CommentChapterRepository(_context);
            }
            return _commentChapterRepository;
        }
    }
    private ICommentStoryRepository _commentStoryRepository;
    public ICommentStoryRepository CommentStoryRepository
    {
        get
        {
            if (_commentStoryRepository == null)
            {
                _commentStoryRepository = new CommentStoryRepository(_context);
            }
            return _commentStoryRepository;
        }
    }
    private  IChapterRepository _chapterRepository;
    public IChapterRepository ChapterRepository
    {
        get
        {
            if (_chapterRepository == null)
            {
                _chapterRepository = new ChapterRepository(_context);
            }
            return _chapterRepository;
        }
    }
    private IReadingHistoryRepository _readingHistoryRepository;
    public IReadingHistoryRepository ReadingHistoryRepository
    {
        get
        {
            if (_readingHistoryRepository == null)
            {
                _readingHistoryRepository = new ReadingHistoryRepository(_context);
            }
            return _readingHistoryRepository;
        }
    }

    private IGenreRepository _genreRepository;
    public IGenreRepository GenreRepository
    {
        get
        {
            if(_genreRepository == null)
            {
                _genreRepository = new GenreRepository(_context);
            }    
            return _genreRepository;
        }
    }
    private IUserFollowStoryRepository _userFollowStoryRepository;
    public IUserFollowStoryRepository UserFollowStoryRepository
    {
        get
        {
            if(_userFollowStoryRepository == null)
            {
                _userFollowStoryRepository = new UserFollowStoryRepository(_context);
            }
            return _userFollowStoryRepository;
        }
    }
    private INotificationRepository _notificationRepository;
    public INotificationRepository NotificationRepository
    {
        get
        {
            if (_notificationRepository is null)
            {
                _notificationRepository = new NotificationRepository(_context);
            }
            return _notificationRepository;
        }
    }
    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public DbContext GetDbContext()
    {
        return _context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync();
    }
}
