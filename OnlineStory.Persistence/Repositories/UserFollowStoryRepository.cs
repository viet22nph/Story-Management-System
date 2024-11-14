
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions.Repository;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.ApplicationDbContext;

namespace OnlineStory.Persistence.Repositories;

public class UserFollowStoryRepository : GenericRepository<UserStoryTracking>, IUserFollowStoryRepository
{
    public UserFollowStoryRepository(AppDbContext context) : base(context)
    {
    }
}
