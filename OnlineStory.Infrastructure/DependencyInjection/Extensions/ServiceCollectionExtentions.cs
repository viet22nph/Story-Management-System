

using Infrastructure.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineStory.Application.Abstractions.Services;
using OnlineStory.Infrastructure.Authentication;
using OnlineStory.Infrastructure.DependencyInjection.Options;
using OnlineStory.Infrastructure.Services;

namespace OnlineStory.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtentions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Configure
        services.Configure<RedisOptions>(configuration.GetSection("RedisOptions"));
        #endregion

        #region Services
        services.AddScoped(sp => sp.GetService<IOptionsSnapshot<RedisOptions>>().Value);
        services.AddScoped<IRedisConnectionWrapper, RedisConnectionWrapper>();
        services.AddScoped<ICacheManager, RedisCacheManager>();
        #endregion

        // add jwt service
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IImageStorageService, LocalImageStorageService>();
    }
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                JwtOptions jwtOptions = new JwtOptions();
                configuration.GetSection(nameof(JwtOptions)).Bind(jwtOptions);
                var key = System.Text.Encoding.UTF8.GetBytes(jwtOptions.SecretKey);
                // storing the JWT in the AuthenticationProperties allows you to retrieve it from elsewhere withinyour application
                // example var  accesstoken = await HttpContext.GetTokenAsync(access_token);
                o.SaveToken = true;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = false, // on production is true
                    ValidateIssuer = false, // on production is true
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero,
                };
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Append("IS-TOKEN-EXPRIED", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization();
    }
}
