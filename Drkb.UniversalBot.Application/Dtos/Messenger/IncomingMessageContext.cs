namespace Drkb.UniversalBot.Application.Dtos.Messenger;

using Domain = Domain.Entity.ValueObjects;

public record IncomingMessageContext
{
    public required long ConversationId { get; init; }
    public required long SenderId { get; init; }
    public required Domain.Messenger Messenger { get; init; }
    public string? Text { get; init; }
    public string? CallbackEventId { get; init; }
    public string? CallbackPayloadId { get; init; }
    
    public bool IsCallback => CallbackEventId is not null;
}