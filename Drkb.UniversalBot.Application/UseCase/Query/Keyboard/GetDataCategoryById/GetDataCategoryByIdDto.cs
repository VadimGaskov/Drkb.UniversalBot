using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;

public class GetDataCategoryByIdDto
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Value { get; set; }
    public List<Category> Categories { get; set; }
}