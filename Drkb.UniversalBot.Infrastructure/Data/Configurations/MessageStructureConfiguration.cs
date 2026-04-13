using Drkb.UniversalBot.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drkb.UniversalBot.Infrastructure.Data.Configurations;

public class MessageStructureConfiguration: IEntityTypeConfiguration<MessageStructure>
{
    public void Configure(EntityTypeBuilder<MessageStructure> builder)
    {
        builder.HasIndex(x => new { x.Title, x.CategoryId }).IsUnique();
    }
}