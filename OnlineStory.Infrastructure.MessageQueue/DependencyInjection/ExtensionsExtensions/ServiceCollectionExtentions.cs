using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStory.Infrastructure.MessageQueue.DependencyInjection.Configuration;

namespace OnlineStory.Infrastructure.MessageQueue.DependencyInjection.ExtensionsExtensions;

public static class ServiceCollectionExtentions
{
    public static void AddMasstransitRabbitMQServices(this IServiceCollection services, IConfiguration configuration)
    {
        var masstransitConfiguration = new MasstransitConfiguration();
        configuration.GetSection("MasstransitConfiguration").Bind(masstransitConfiguration);
        services.Configure<MasstransitConfiguration>(configuration.GetSection("MasstransitConfiguration"));
        services.AddMassTransit(mt =>
        {
            mt.UsingRabbitMq((context, bus) =>
            {
                bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h =>
                {
                    h.Username(masstransitConfiguration.UserName);
                    h.Password(masstransitConfiguration.Password);
                });
                bus.ReceiveEndpoint(masstransitConfiguration.NotificationQueue, e =>
                {
                    // Đăng ký các Consumer tại đây
                });
            });
        });
    }
}
