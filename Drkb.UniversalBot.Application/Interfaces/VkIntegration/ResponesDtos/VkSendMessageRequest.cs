namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;

public class VkSendMessageRequest
{
    public long PeerId { get; init; }
    public string? Text { get; init; }
    public string? KeyboardPayload { get; init; }
    public IReadOnlyList<string> FilePaths { get; init; } = [];

    public bool HasText => !string.IsNullOrWhiteSpace(Text);
    public bool HasKeyboard => !string.IsNullOrWhiteSpace(KeyboardPayload);
    public bool HasFiles => FilePaths is { Count: > 0 };
}