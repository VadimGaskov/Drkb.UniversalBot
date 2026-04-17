namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategory;

public record GetCategoryDto
{
    public string Name { get; init; } = null!;
    public Guid Id { get; init; }
    public string? Value { get; init; }
    public Guid? ParentCategoryId { get; init; }
}