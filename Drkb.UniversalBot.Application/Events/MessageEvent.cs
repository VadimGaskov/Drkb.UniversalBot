using System.Text.Json;
using System.Text.Json.Serialization;
using MessageBroker.Abstractions;

namespace Drkb.UniversalBot.Application.Events;

public record MessageEvent: BaseIntegrationEvent
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("group_id")]
    public long GroupId { get; set; }

    [JsonPropertyName("secret")]
    public string? Secret { get; set; }

    [JsonPropertyName("object")]
    public JsonElement Object { get; set; }
}