
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions.Repository;
using OnlineStory.Contract.Dtos.CommentDtos;
using OnlineStory.Domain.Entities;
using OnlineStory.Domain.Enums;
using OnlineStory.Persistence.ApplicationDbContext;

namespace OnlineStory.Persistence.Repositories;

public class CommentChapterRepository : GenericRepository<Comment, int>, ICommentChapterRepository
{
    public CommentChapterRepository(AppDbContext context) : base(context)
    {
    }
    public async Task AddCommentAsync(Comment comment)
    {
        int newRight, newLeft;
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {

            if (comment.ParentCommentId.HasValue)
            {
                var parent = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.ParentCommentId.Value);
                if (parent == null) throw new KeyNotFoundException("Parent comment not found.");

                var rightValue = parent.Right;
                UpdateBoundariesForInsert(rightValue, comment.Type, comment.ChapterId);
                newRight = rightValue + 1;
                newLeft = rightValue;
            }
            else
            {
                var maxRight = await _context.Comments
                    .Where(x => x.Type == comment.Type && x.ChapterId == comment.ChapterId)
                    .MaxAsync(c => (int?)c.Right) ?? 0;
                newLeft = maxRight + 1;
                newRight = newLeft + 1;
            }

            comment.Left = newLeft;
            comment.Right = newRight;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    private void UpdateBoundariesForInsert(int rightBoundary, CommentType type, int? chapterId)
    {
        // Increase the left and right boundaries of the existing comments
        var commentsToUpdate = _context.Comments
            .Where(c => c.Type == type && c.ChapterId == chapterId)
            .ToList();

        foreach (var comment in commentsToUpdate)
        {
            if (comment.Right >= rightBoundary)
            {
                comment.Right += 2; // Shift right by 2 for new node
            }
            if (comment.Left > rightBoundary)
            {
                comment.Left += 2; // Shift left by 2 for new node
            }
        }

        // Update the modified comments in the context
        _context.SaveChanges();
    }

    public async Task<List<CommentDto>> GetCommentByParentIdAsync(int chapterId, int? parentId = null, int offset = 0, int limit = 50)
    {
        if (parentId is not null)
        {
            var commentParent = await _context.Comments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == parentId);
            if (commentParent is null)
            {
                throw new Exception("Not found");
            }
            IQueryable<Comment> query = _context.Comments.AsNoTrackingWithIdentityResolution();

            query = query.Where(x => x.Type == CommentType.Story && x.ChapterId == chapterId && x.ParentCommentId == parentId && x.Left >= commentParent.Left && x.Right <= commentParent.Right);

            // Sắp xếp và phân trang
            var comments = await query
                .OrderByDescending(x => x.Left)
                .Skip(offset)
                .Take(limit)
                .Select(x => new CommentDto
                {
                    Id = x.Id,
                    ParentId = x.ParentCommentId,
                    Content = x.Content,
                    Left = x.Left,
                    Right = x.Right,
                    Author = new CommentUserDto
                    {
                        UserId = x.User.Id,
                        AvatarUrl = x.User.Avatar,
                        DisplayName = x.User.DisplayName,
                    },
                })
                .ToListAsync();

            return comments;
        }
        else
        {
            IQueryable<Comment> query = _context.Comments.AsNoTrackingWithIdentityResolution();

            query = query.Where(x => x.Type == CommentType.Story && x.ChapterId == chapterId&& x.ParentCommentId == parentId);
            // Sắp xếp và phân trang
            var comments = await query
                .OrderBy(x => x.Left)
                .Skip(offset)
                .Take(limit)
                .Select(x => new CommentDto
                {
                    Id = x.Id,
                    ParentId = x.ParentCommentId,
                    Content = x.Content,
                    Left = x.Left,
                    Right = x.Right,
                    Author = new CommentUserDto
                    {
                        UserId = x.User.Id,
                        AvatarUrl = x.User.Avatar,
                        DisplayName = x.User.DisplayName,
                    },
                })
                .ToListAsync();

            return comments;
        }

    }
    public async Task<int> CountCommentByParentIdAsync(int chapterId, int? parentId = null)
    {
        IQueryable<Comment> query = _context.Comments.AsNoTrackingWithIdentityResolution();
        query = query.Where(x => x.Type == CommentType.Story && x.ChapterId == chapterId && x.ParentCommentId == parentId);
        return await query.CountAsync();
    }

    public async Task RemoveCommentAsync(int commentId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {

            var comment = await _context.Comments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment is null)
            {
                throw new ArgumentException("Comment not found");
            }
            int leftValue = comment.Left, rightValue = comment.Right;
            int width = rightValue - leftValue + 1;
            // xóa tất cả các comment con cua story duoc chon
            var listCommentsRemove = await _context.Comments
                .Where(x => x.Type == CommentType.Story && x.ChapterId == comment.ChapterId && x.Left >= leftValue && x.Right <= rightValue)
                .ToListAsync();
            _context.Comments.RemoveRange(listCommentsRemove);
            _context.SaveChanges();
            // cập nhật lại các giá trị left right
            var listComment = await _context.Comments
                .Where(x => x.Type == CommentType.Story && x.ChapterId == comment.ChapterId)
                .ToListAsync();
            foreach (var item in listComment)
            {
                if (item.Right > rightValue)
                {
                    item.Right -= width; // Shift right by 2 for new node
                }
                if (item.Left > rightValue)
                {
                    item.Left -= width; // Shift left by 2 for new node
                }
            }

            _context.Comments.UpdateRange(listComment);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message);
        }

    }
}
