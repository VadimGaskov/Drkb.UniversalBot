using System.Text.Json.Serialization;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;

public sealed class VkDocsSaveResponse
{
    [JsonPropertyName("response")]
    public VkDocument Response { get; set; }
}

public sealed class VkDocument
{
    [JsonPropertyName("doc")]
    public VkDocumentInfo Doc { get; set; }
}

public sealed class VkDocumentInfo
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("owner_id")]
    public long OwnerId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";
}