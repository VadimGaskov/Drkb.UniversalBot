using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.DI;

public static class DataProviderServiceCollectionExtention
{
    public static IServiceCollection AddDataProviderServices(this IServiceCollection service)
    {
        service.Scan(scan => scan
            .FromAssemblyOf<InfrastructureAssemblyMarker>()
            .AddClasses(classes => classes.AssignableTo<IDataProviderMarker>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        return service;
    }
}