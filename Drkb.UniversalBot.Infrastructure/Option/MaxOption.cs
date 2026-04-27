namespace Drkb.UniversalBot.Infrastructure.Option;

public class MaxOption
{
    public string SubscriptionUrl { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public List<string> UpdateTypes { get; set; } = [];
    public string Secret { get; set; } = null!;
}