
using OnlineStory.Application.Abstractions.Repository;
using OnlineStory.Domain.Abstractions.RepositoryBase;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.ApplicationDbContext;

namespace OnlineStory.Persistence.Repositories;

public class GenreRepository : GenericRepository<Genre, int>, IGenreRepository
{
    public GenreRepository(AppDbContext context) : base(context)
    {
    }
}
