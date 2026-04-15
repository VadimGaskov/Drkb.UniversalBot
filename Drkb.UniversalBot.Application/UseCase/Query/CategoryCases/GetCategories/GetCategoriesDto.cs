namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategories;

public record GetCategoriesDto
{
    public string Name { get; init; } = null!;
    public Guid Id { get; init; }
}