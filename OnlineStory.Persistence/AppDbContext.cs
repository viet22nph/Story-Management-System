
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OnlineStory.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStory.Domain.Abstractions.Entities;
using OnlineStory.Domain.Entities;
using System.Linq.Expressions;
using System.Reflection;
using OnlineStory.Persistence.Configurations;
using Domain.Entities;

namespace OnlineStory.Persistence.ApplicationDbContext;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        var softDeleteEntites = typeof(ISoftDelete).Assembly.GetTypes()
            .Where(t => typeof(ISoftDelete).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        foreach (var softDeleteEntity in softDeleteEntites)
        {
            builder.Entity(softDeleteEntity)
                .HasQueryFilter(generateQueryFilterLambda(softDeleteEntity));
            builder.Entity(softDeleteEntity).HasIndex("IsDeleted")
            .HasFilter("[IsDeleted] = 0");
        }

        var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
            (type.BaseType?.IsGenericType ?? false) &&
            (type.BaseType.GetGenericTypeDefinition() == typeof(MappingEntityTypeConfiguration<>))
        );
        foreach (var item in typeConfigurations)
        {
            var configuration = (IMappingConfiguration)Activator.CreateInstance(item);
            configuration.ApplyConfiguration(builder);
        }
    }
    private LambdaExpression? generateQueryFilterLambda(Type type)
    {
        var parameter = Expression.Parameter(type, "w");
        var falseContantValue = Expression.Constant(false);
        var propertyAccess = Expression.PropertyOrField(parameter, nameof(ISoftDelete.IsDeleted));
        var equalExpression = Expression.Equal(propertyAccess, falseContantValue);
        var lambda = Expression.Lambda(equalExpression, parameter);
        return lambda;
    }
    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var addEntities = ChangeTracker.Entries<IDateTracking>()
           .Where(E => E.State == EntityState.Added)
           .ToList();

        addEntities.ForEach(E =>
        {
            E.Entity.CreatedDate = DateTimeOffset.UtcNow;
        });

        var EditedEntities = ChangeTracker.Entries<IDateTracking>()
            .Where(E => E.State == EntityState.Modified)
            .ToList();

        EditedEntities.ForEach(E =>
        {
            E.Entity.ModifiedDate = DateTimeOffset.UtcNow;
        });
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<AppUserRole> AppUserRole { get; set; }
    public DbSet<Domain.Entities.Story> Stories { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<ChapterImage> ChaptersImages { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<StoryGenre> StoryGenres { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Domain.Entities.Identity.Action> Actions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<StoryView> StoriesViews { get; set; }
    public DbSet<ReadingHistory> ReadingHistories { get; set; }
    public DbSet<UserStoryTracking> UserStoryTrackings { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Notification> Notification { get; set; }
    public DbSet<UserNotification> UserNotifications { get; set; }
}
