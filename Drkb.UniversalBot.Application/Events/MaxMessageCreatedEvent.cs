using System.Text.Json;
using System.Text.Json.Serialization;
using MassTransit;

namespace Drkb.UniversalBot.Application.Events;

[EntityName("vk.message.created.v1")]
public record MaxMessageCreatedEvent
{
    [JsonPropertyName("update_type")]
    public string UpdateType { get; set; } = null!;

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }
    
    [JsonPropertyName("message")]
    public JsonElement Message { get; set; }
}