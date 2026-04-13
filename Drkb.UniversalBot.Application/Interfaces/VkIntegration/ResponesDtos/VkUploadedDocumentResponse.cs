using System.Text.Json.Serialization;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;

public sealed class VkUploadedDocumentResponse
{
    [JsonPropertyName("file")]
    public string File { get; set; } = null!;
}