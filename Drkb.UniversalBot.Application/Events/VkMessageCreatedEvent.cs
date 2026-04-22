using System.Text.Json;
using System.Text.Json.Serialization;
using MassTransit;

namespace Drkb.UniversalBot.Application.Events;

[EntityName("message.created.v1")]
public record VkMessageCreatedEvent
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