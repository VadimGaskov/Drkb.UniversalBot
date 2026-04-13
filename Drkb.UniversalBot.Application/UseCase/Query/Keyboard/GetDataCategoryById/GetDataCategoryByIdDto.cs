using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;

public class GetDataCategoryByIdDto
{
    public string? Text { get; set; }
    public List<Category> Categories { get; set; }
    public List<string?> PathFiles { get; set; }
}