using System.Text.Json.Serialization;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;

public sealed class VkUploadServerResponse
{
    [JsonPropertyName("response")]
    public VkUploadServerData Response { get; set; } = null!;
}

public sealed class VkUploadServerData
{
    [JsonPropertyName("upload_url")]
    public string UploadUrl { get; set; } = null!;
}