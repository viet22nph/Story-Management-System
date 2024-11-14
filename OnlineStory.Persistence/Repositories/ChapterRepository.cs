
using OnlineStory.Application.Abstractions.Repository;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.ApplicationDbContext;

namespace OnlineStory.Persistence.Repositories;

public class ChapterRepository : GenericRepository<Chapter, int>, IChapterRepository
{
    public ChapterRepository(AppDbContext context) : base(context)
    {
    }
}
