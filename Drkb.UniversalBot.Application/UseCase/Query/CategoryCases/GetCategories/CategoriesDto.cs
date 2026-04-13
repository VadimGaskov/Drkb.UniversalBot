namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategories;

public record CategoriesDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<CategoriesDto> CategoryChildren { get; set; } = [];
}