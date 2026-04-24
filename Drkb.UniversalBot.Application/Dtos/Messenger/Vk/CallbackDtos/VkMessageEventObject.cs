using System.Text.Json;
using System.Text.Json.Serialization;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;

public class VkPayload
{
    [JsonPropertyName("command")]
    public string Command { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}

public class VkMessageEventObject
{
    [JsonPropertyName("event_id")]
    public string EventId { get; set; } = string.Empty;

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("peer_id")]
    public long PeerId { get; set; }

    [JsonPropertyName("conversation_message_id")]
    public int ConversationMessageId { get; set; }

    [JsonPropertyName("payload")]
    public VkPayload Payload { get; set; }
}