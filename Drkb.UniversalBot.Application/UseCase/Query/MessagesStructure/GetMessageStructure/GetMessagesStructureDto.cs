using Drkb.UniversalBot.Domain.Entity.ValueObjects;

namespace Drkb.UniversalBot.Application.UseCase.Query.MessagesStructure.GetMessageStructure;

public record MessagesStructureDto
{
    public string? Title { get; set; }
    public string? Value { get; set; }
    public int Seq { get; set; }
    public TypeField TypeField { get; set; }
    public string? OriginalFileName { get; set; }
}

public record GetMessagesStructureDto
{
    public Guid CategoryId { get; init; }
    public string CategoryTitle { get; init; } = null!;
    public List<MessagesStructureDto> MessagesStructureDtos { get; set; } = [];
}