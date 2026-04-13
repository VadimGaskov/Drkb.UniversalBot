using Drkb.UniversalBot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.DI;

public static class DbContextServiceCollectionExtention
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<BotDbContext>(options =>
        {
            options.UseNpgsql(connectionString, x => x.MigrationsAssembly("Drkb.UniversalBot.Infrastructure"));
        });

        return services;
    }
}