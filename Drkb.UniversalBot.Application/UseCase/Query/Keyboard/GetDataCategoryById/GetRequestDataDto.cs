namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;

public record GetRequestDataDto
{
    public string? Name { get; set; }
    public string? Value { get; set; }
    public string? Keyboard { get; set; }
    public List<string> FilesUrl { get; set; } = [];
}