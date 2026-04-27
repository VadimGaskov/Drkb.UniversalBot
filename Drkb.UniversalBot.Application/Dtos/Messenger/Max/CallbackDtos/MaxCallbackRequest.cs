using System.Text.Json;
using System.Text.Json.Serialization;

namespace Drkb.UniversalBot.Application.Dtos.Messenger.Max.CallbackDtos;

public record MaxCallbackRequest
{
    [JsonPropertyName("update_type")] 
    public string UpdateType { get; set; } = null!;

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }
    
    [JsonPropertyName("message")]
    public JsonElement Message { get; set; }
}