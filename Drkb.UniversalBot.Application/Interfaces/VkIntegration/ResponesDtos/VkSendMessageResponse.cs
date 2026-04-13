using System.Text.Json.Serialization;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;

public class VkSendMessageResponse
{
    [JsonPropertyName("message_id")]
    public int MessageId { get; set; }
}