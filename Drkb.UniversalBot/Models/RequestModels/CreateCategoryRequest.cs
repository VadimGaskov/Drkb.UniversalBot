using Drkb.UniversalBot.Domain.Entity.ValueObjects;

namespace Drkb.UniversalBot.Models.RequestModels;

public record CreateMessageStructureItemRequest
{
    public string? Name { get; init; }
    public string? Value { get; init; }
    public IFormFile? File { get; init; }
    public int Seq { get; init; }
    public TypeField TypeField { get; init; }
}

public record CreateCategoryRequest
{

    public string NameCategory { get; set; } = null!;
    public Guid? ParentCategoryId { get; set; }
    public List<CreateMessageStructureItemRequest> Items { get; init; } = [];
}