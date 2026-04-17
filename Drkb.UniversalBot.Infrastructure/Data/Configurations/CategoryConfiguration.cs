using Drkb.UniversalBot.Domain.Entity;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drkb.UniversalBot.Infrastructure.Data.Configurations;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(x=> new {x.Title, x.CategoryStatus}).IsUnique();
        
        builder.HasOne(x => x.ParentCategory)
            .WithMany(x => x.ChildrenCategories)
            .HasForeignKey(x => x.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x=>x.CategoryStatus)
            .HasConversion<string>()
            .HasDefaultValue(CategoryStatus.Active);
    }
}