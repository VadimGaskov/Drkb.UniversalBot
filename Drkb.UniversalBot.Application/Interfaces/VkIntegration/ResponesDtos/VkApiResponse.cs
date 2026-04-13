using System.Text.Json.Serialization;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;

public class VkApiResponse<T>
{
    [JsonPropertyName("response")]
    public T? Response { get; set; }

    [JsonPropertyName("error")]
    public VkApiError? Error { get; set; }
}