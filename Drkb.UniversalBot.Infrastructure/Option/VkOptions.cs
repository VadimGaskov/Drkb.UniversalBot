namespace Drkb.UniversalBot.Infrastructure.Option;

public class VkOptions
{
    public string AccessToken { get; set; } = null!;
    public string ApiVersion { get; set; } = "5.199";
    public long GroupId { get; set; }
    public string ConfirmationCode { get; set; } = null!;
    public string Secret { get; set; } = null!;
}