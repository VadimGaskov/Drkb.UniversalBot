using System.Text.Json.Serialization;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;

public class VkMessageNewObject
{
    [JsonPropertyName("message")]
    public VkMessage? Message { get; set; }
}