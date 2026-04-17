namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;

public class VkSendMessageRequest
{
    public long PeerId { get; init; }
    public string? Value { get; init; }
    public string? Name { get; set; }
    public string? KeyboardPayload { get; init; }
    public bool SendText { get; set; } = true;
    public IReadOnlyList<string> FilesUrl { get; init; } = [];

    public bool HasValue => !string.IsNullOrWhiteSpace(Value);
    public bool HasName => !string.IsNullOrWhiteSpace(Name);
    public bool HasKeyboard => !string.IsNullOrWhiteSpace(KeyboardPayload);
    public bool HasFiles => FilesUrl is { Count: > 0 };
}