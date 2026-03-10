using AlphaAuraChat.Application.Abstractions.Behaviors;
using AlphaAuraChat.Domain.Tenants.Services;
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
        services.AddTransient<IKeyGenerationService, KeyGenerationService>();
        return services;
    }
}
