using Drkb.UniversalBot.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.DI;

public static class BehaviorServiceCollectionExtention
{
    public static IServiceCollection AddBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingAndErrorHandlingBehavior<,>));
        return services;
    }
}