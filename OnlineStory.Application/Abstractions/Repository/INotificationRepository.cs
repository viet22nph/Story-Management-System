
using OnlineStory.Domain.Abstractions.RepositoryBase;
using OnlineStory.Domain.Entities;

namespace OnlineStory.Application.Abstractions.Repository;

public interface INotificationRepository: IGenericRepository<Notification, int>
{
}
