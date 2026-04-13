namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;

public record GetRequestDataDto
{
    public string? Text { get; set; }
    public string? Keyboard { get; set; }
    public List<string> Files { get; set; } = [];
}