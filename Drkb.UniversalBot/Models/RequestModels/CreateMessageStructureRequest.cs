using Drkb.UniversalBot.Domain.Entity.ValueObjects;

namespace Drkb.UniversalBot.Models.RequestModels;

public record CreateMessageStructureItemRequest
{
    public string? Title { get; init; }
    public string? Value { get; init; }
    public IFormFile? File { get; init; }
    public int Seq { get; init; }
    public TypeField TypeField { get; init; }
}

public record CreateMessageStructureRequest
{
    public Guid CategoryId { get; init; }
    public List<CreateMessageStructureItemRequest> Items { get; init; } = [];
}