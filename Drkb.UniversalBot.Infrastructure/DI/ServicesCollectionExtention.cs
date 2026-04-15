using System.Text.Json.Serialization;
using Drkb.CacheService.Redis;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.Authorization;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.Options;
using Drkb.UniversalBot.Infrastructure.Options;
using Drkb.UniversalBot.Infrastructure.Services;
using Drkb.UniversalBot.Infrastructure.Services.Authorization;
using Drkb.UniversalBot.Infrastructure.Services.VkIntegration;
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
        
        services.Configure<LocalFileStorageOptions>(
            configuration.GetSection("LocalFileStorage"));

        services.AddScoped<IFileStorage, LocalFileStorage>();
        
        services.Configure<VkOptions>(configuration.GetSection("Vk"));
        services.AddHttpClient<IVkApiClient, VkApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.vk.com/method/");
        });

        services.AddScoped<IVkMessageService, VkMessageService>();
        services.AddTransient<IVkKeyboardFactory, VkKeyboardFactory>();

        services.RegisterRedis(configuration.GetSection("Redis"));

        services.AddScoped<IVkMessageEventProcessor, VkMessageEventProcessor>();
        services.AddScoped<IVkMessageNewProcessor, VkMessageNewProcessor>();
        
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters
                    .Add(new JsonStringEnumConverter());
            });
        
        return services;
    }
}