namespace Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;

public class StoredFileResult
{
    public string FileName { get; init; } = null!;
    public string RelativePath { get; init; } = null!;
    public string ContentType { get; init; } = null!;
    public long Length { get; init; }
}