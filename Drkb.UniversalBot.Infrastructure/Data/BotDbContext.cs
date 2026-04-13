using Drkb.UniversalBot.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Drkb.UniversalBot.Infrastructure.Data;

public class BotDbContext: DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<Statistics> Statistics { get; set; }
    public DbSet<MessageStructure> MessageStructures { get; set; }
    public DbSet<SenderUser> SenderUsers { get; set; }
    
    public BotDbContext(DbContextOptions<BotDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfrastructureAssemblyMarker).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>().HaveConversion<string>();
        base.ConfigureConventions(configurationBuilder);
    }
}