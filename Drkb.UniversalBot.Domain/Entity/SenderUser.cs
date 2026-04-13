using Drkb.UniversalBot.Domain.Entity.ValueObjects;

namespace Drkb.UniversalBot.Domain.Entity;

public class SenderUser: BaseEntity
{
    public Messenger Messenger { get; set; }
    public string ExternalId { get; set; }
    public List<Statistics> Statistics { get; set; }
    public List<Recommendation> Recommendations { get; set; }
}