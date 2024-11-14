
using OnlineStory.Application.Abstractions.Repository;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.ApplicationDbContext;

namespace OnlineStory.Persistence.Repositories;

public class ReadingHistoryRepository : GenericRepository<ReadingHistory, int>, IReadingHistoryRepository
{
    public ReadingHistoryRepository(AppDbContext context) : base(context)
    {
    }
}
