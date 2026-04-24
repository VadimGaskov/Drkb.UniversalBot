namespace Drkb.UniversalBot.Application.Dtos.Messenger;

public record SendMessageContext
{
    public required long ConversationId { get; init; }
    public required long SenderId { get; init; }
    public string? Text { get; init; }
    public string? Keyboard { get; init; }
    public IReadOnlyList<string> FilesUrl { get; init; } = [];
    public Domain.Entity.ValueObjects.Messenger Messenger { get; init; }

    public bool HasText => !string.IsNullOrEmpty(Text);
    public bool HasKeyboard => !string.IsNullOrEmpty(Keyboard);
    public bool HasFiles => FilesUrl.Any();
}