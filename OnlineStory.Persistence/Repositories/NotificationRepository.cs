
using OnlineStory.Application.Abstractions.Repository;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.ApplicationDbContext;

namespace OnlineStory.Persistence.Repositories;

public class NotificationRepository : GenericRepository<Notification, int>, INotificationRepository
{
    public NotificationRepository(AppDbContext context) : base(context)
    {
    }
}
