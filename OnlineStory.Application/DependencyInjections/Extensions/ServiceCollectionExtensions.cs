
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnlineStory.Application.Behaviors;

namespace Application.DependencyInjections.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMediatRApplication(this IServiceCollection services)
        {

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));
        }
      


    }
}
