using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.DI;

public static class LoggerServiceCollectionExtention
{
    public static IServiceCollection AddSerilogLogger(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, SerilogLoggerService>();
        return services;
    }
}