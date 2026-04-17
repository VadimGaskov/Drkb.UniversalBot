using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Contracts.Category;
using Drkb.UniversalBot.Integration.RabbitMq;
using MessageBroker.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.DI;

public static class RabbitMQServiceCollection
{
    public static IServiceCollection AddRabbitMQCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRabbitMq(configuration.GetSection("RabbitMQ"), configure =>
        {
            configure.ConfigureConsumer(x =>
            {
                x.Bind<VkMessageEvent>(
                    exchange: "messages-events",
                    queueName: "bot.messages-events.created",
                    routingKey: "messages-events.created");
            });
            
            configure.ConfigureProducer(x =>
            {
                x.Map<VkMessageEvent>(
                    exchange: "messages-events",
                    routingKey: "messages-events.created");
                
                x.Map<CategoryCreatedEvent>(
                    exchange: CategoryIntegrationMetadata.Created.Exchange,
                    routingKey: CategoryIntegrationMetadata.Created.RoutingKey);
                
                x.Map<CategoryUpdatedEvent>(
                    exchange: CategoryIntegrationMetadata.Updated.Exchange,
                    routingKey: CategoryIntegrationMetadata.Updated.RoutingKey);
            });
        });
        return services;
    }
}