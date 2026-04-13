namespace Drkb.UniversalBot.Domain.Entity;

public class Recommendation: BaseEntity
{
    public string Value { get; set; }
    public Guid UserId { get; set; }
    public SenderUser SenderUser { get; set; }
}