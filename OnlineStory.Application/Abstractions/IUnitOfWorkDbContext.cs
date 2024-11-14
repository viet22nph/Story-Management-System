

using Microsoft.EntityFrameworkCore;

namespace OnlineStory.Domain.Abstractions;

public interface IUnitOfWorkDbContext<TContext> : IAsyncDisposable where TContext : DbContext
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    TContext GetDbContext();
}
