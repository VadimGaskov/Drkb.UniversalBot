using System.Text.Json.Serialization;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;

public class VkMessage
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("from_id")]
    public long FromId { get; set; }

    [JsonPropertyName("peer_id")]
    public long PeerId { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}

public class VkMessageNewObject
{
    [JsonPropertyName("message")]
    public VkMessage? Message { get; set; }
}