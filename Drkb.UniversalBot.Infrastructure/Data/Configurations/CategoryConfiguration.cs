using Drkb.UniversalBot.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drkb.UniversalBot.Infrastructure.Data.Configurations;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(x=> x.Title).IsUnique();
        
        builder.HasOne(x => x.ParentCategory)
            .WithMany(x => x.ChildrenCategories)
            .HasForeignKey(x => x.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}