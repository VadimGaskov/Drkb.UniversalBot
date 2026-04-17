namespace Drkb.UniversalBot.Application.Dtos;

public record GetAllByFilesUrlDto
{
    public Guid Id { get; init; }
    public string Url { get; init; } 
    public string Title { get; init; }
    public string Key { get; init; }
    public bool IsDeleted { get; init; }
}