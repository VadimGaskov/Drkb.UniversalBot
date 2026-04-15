namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategoriesTree;

public record CategoriesTreeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CategoriesTreeDto> CategoryChildren { get; set; } = [];
}