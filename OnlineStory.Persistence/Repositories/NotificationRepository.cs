
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions.Repository;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.ApplicationDbContext;

namespace OnlineStory.Persistence.Repositories;

public class NotificationRepository : GenericRepository<Notification, int>, INotificationRepository
{
    public NotificationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Notification> GetBatchNotificationCommentAsync(Guid userId, int commentParent)
    {
       var notification = await _context.Notification
            .Where(x => x.UserReceiveId == userId && x.IsBatch == true && x.RelatedData.Contains(commentParent.ToString()))
            .OrderByDescending(x=> x.CreatedDate)
            .FirstOrDefaultAsync();
        return notification;
    }
    public async Task<Notification> GetBatchNotificationStoryAsync(Guid userId, Guid storyId)
    {
        var notification = await _context.Notification
            .Where(x =>x.UserReceiveId == userId && x.IsBatch == true && x.RelatedData.Contains(storyId.ToString()))
             .OrderByDescending(x => x.CreatedDate)
             .FirstOrDefaultAsync();
        return notification;
    }
    public async Task<List<Notification>> GetBatchNotificationsForUsersAsync(List<Guid> userIds, Guid storyId)
    {
        var notifications =  await _context.Notification
            .Where(x => x.IsBatch == true
                        && userIds.Contains(x.UserReceiveId)  // Kiểm tra xem UserReceiveId có trong userIds không
                        && x.RelatedData.Contains(storyId.ToString()))  // Kiểm tra xem storyId có trong RelatedData không
            .ToListAsync();
        return notifications;
    }
}
