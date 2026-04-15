namespace Drkb.UniversalBot.Domain.Entity;

public class Statistics: BaseEntity
{
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime Date { get; set; }
    public Guid SenderUserId { get; set; }
    public SenderUser SenderUser { get; set; }
}