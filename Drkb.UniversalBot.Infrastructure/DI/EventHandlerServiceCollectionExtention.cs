using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Infrastructure.Services.MessageBrocker.MessageEvents;
using MessageBroker.Abstractions.Interfaces.Consumer;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.DI;

public static class EventHandlerServiceCollectionExtention
{
    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IEventHandler<MessageEvent>, CreateMessageEventHandler>();
        return services;
    }
}