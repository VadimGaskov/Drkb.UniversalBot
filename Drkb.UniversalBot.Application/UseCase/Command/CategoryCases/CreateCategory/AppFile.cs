namespace Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;

public record AppFile
{
    public string FileName { get; init; } = null!;
    public string ContentType { get; init; } = null!;
    public byte[] Content { get; init; } = [];
    public long Length { get; init; }
}