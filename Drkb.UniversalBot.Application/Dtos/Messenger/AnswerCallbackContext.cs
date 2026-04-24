namespace Drkb.UniversalBot.Application.Dtos;

public record AnswerCallbackContext
{
    public required string EventId { get; set; }
    public required long SenderId { get; set; }
    public required long ConversationId { get; init; }
}