using Drkb.MessageBroker.Masstransit;
using Drkb.UniversalBot.Infrastructure.Data;
using Drkb.UniversalBot.Infrastructure.Services.MessageBrocker.MessageEvents.Vk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.DI;

public static class RabbitMQServiceCollection
{
    public static IServiceCollection AddRabbitMQCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDrkbMassTransit<BotDbContext>(configuration.GetSection("RabbitMQ"), options =>
        {
            options.DomainName = "bot";

            options.ConfigureRegistration = x =>
            {
                x.AddConsumer<CreateMessageConsumer>();
            };
        });
        
        return services;
    }
}