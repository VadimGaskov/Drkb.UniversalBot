using System.Text.Json.Serialization;
using Drkb.CacheService.Redis;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.Authorization;
using Drkb.UniversalBot.Application.Interfaces.Messagers;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using Drkb.UniversalBot.Infrastructure.Messengers;
using Drkb.UniversalBot.Infrastructure.Messengers.Max;
using Drkb.UniversalBot.Infrastructure.Messengers.Vk;
using Drkb.UniversalBot.Infrastructure.Option;
using Drkb.UniversalBot.Infrastructure.Services;
using Drkb.UniversalBot.Infrastructure.Services.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.DI;

public static class ServicesCollectionExtention
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters
                    .Add(new JsonStringEnumConverter());
            });
        
        services.Configure<VkOptions>(configuration.GetSection("Vk"));
        services.AddHttpClient<IVkApiClient, VkApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.vk.com/method/");
        });
        
        services.Configure<MaxOption>(configuration.GetSection("Max"));
        
        services.Configure<FileSaverOptions>(configuration.GetSection("FileSaver"));
        services.AddHttpClient<IS3Service, S3Service>();

        services.AddKeyedScoped<IBotService, VkMessageService>(Messenger.VK);
        services.AddKeyedScoped<IBotService, MaxMessageService>(Messenger.MAX);
        
        services.AddTransient<IKeyboardFactory, KeyboardFactory>();

        services.RegisterRedis(configuration.GetSection("Redis"));
        
        services.AddKeyedScoped<IMessageProcessor, TextMessageProcessor>("text");
        services.AddKeyedScoped<IMessageProcessor, CallbackMessageProcessor>("callback");
        
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters
                    .Add(new JsonStringEnumConverter());
            });
        
        return services; 
    }
}