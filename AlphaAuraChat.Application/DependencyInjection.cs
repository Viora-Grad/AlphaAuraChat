using AlphaAuraChat.Application.Abstractions.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace AlphaAuraChat.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configurations =>
        {
            configurations.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configurations.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configurations.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        return services;
    }
}
