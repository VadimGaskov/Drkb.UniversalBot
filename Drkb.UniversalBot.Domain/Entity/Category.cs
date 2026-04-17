using Drkb.UniversalBot.Domain.Entity.ValueObjects;

namespace Drkb.UniversalBot.Domain.Entity;

public class Category: BaseEntity
{
    public string Title { get; set; }
    public string? Value { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Statistics> Statistics { get; set; }
    public List<Category> ChildrenCategories { get; set; } = new();
    public CategoryStatus CategoryStatus { get; set; }
}