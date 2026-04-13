using Drkb.UniversalBot.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.DI;

public static class MediatrServiceCollectionExtention
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(msc => msc.RegisterServicesFromAssemblies(typeof(ApplicationAssemblyMarker).Assembly));
        return services;
    }
}