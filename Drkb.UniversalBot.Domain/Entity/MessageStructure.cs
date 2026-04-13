using Drkb.UniversalBot.Domain.Entity.ValueObjects;

namespace Drkb.UniversalBot.Domain.Entity;

public class MessageStructure: BaseEntity
{
    public string? Title { get; set; }
    public string? Value { get; set; }
    public int Seq { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public TypeField TypeField { get; set; }
    public string? OriginalFileName { get; set; }
    public string? StoredFilePath { get; set; }
    public string? ContentType { get; set; }
}