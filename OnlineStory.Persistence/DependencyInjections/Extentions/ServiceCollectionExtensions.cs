
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnlineStory.Application.Abstractions;
using OnlineStory.Application.Abstractions.Security;
using OnlineStory.Domain.Abstractions.RepositoryBase;
using OnlineStory.Domain.Entities.Identity;
using OnlineStory.Persistence;
using OnlineStory.Persistence.ApplicationDbContext;
using OnlineStory.Persistence.DependencyInjections.Options;
using OnlineStory.Persistence.Repositories;
using OnlineStory.Persistence.Services.Security;
namespace Persistence.DependencyInjections.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServerSqlPersistent(this IServiceCollection services)
        {
            services.AddDbContextPool<DbContext, AppDbContext>((provider, builder) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var optionsMonitor = provider.GetRequiredService<IOptionsMonitor<SqlServerRetryOptions>>();
                var options = optionsMonitor.CurrentValue;

                builder.EnableDetailedErrors(true)
                    .EnableSensitiveDataLogging(true)
                    .UseLazyLoadingProxies(false)
                    .UseSqlServer(
                        connectionString: configuration.GetConnectionString("Connection"), 
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.ExecutionStrategy(dependencies =>
                                new SqlServerRetryingExecutionStrategy(
                                    dependencies: dependencies,
                                    maxRetryCount: options.MaxRetryCount,
                                    maxRetryDelay: options.MaxRetryDelay,
                                    errorNumbersToAdd: options.ErrorNumbersToAdd)
                            );
                            sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name);
                        })
                    .AddInterceptors();
            });

            services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);


        }
        public static void AddIdentityPersister(this IServiceCollection services)
        {
            // add indentity
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = false;
                options.Lockout.MaxFailedAccessAttempts = 5;

            })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<AppDbContext>();
            // config pass
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

            });

        }

        public static void AddRepositoryPersistence(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            
        }
        public static void AddServicePersistence(this IServiceCollection services)
        {
            services.AddScoped<ISecurityService, SecurityService>();


        }
        public static OptionsBuilder<SqlServerRetryOptions> ConfigureSqlServerRetryOptionPersistence(this IServiceCollection services, IConfiguration configuration)
        
          =>  services.AddOptions<SqlServerRetryOptions>()
            .Bind(configuration.GetSection("SqlServerRetryOptions"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
