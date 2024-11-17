
using OnlineStory.Domain.Abstractions.RepositoryBase;
using OnlineStory.Domain.Entities;

namespace OnlineStory.Application.Abstractions.Repository;

public interface INotificationRepository: IGenericRepository<Notification, int>
{
    Task<Notification> GetBatchNotificationCommentAsync(Guid userId, int commentParent);
    Task<Notification> GetBatchNotificationStoryAsync(Guid userId, Guid storyId);
    Task<List<Notification>> GetBatchNotificationsForUsersAsync(List<Guid> userIds, Guid storyId);
}
