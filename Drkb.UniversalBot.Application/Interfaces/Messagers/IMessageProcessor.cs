using Drkb.UniversalBot.Application.Dtos.Messenger;

namespace Drkb.UniversalBot.Application.Interfaces.Messagers;

public interface IMessageProcessor
{
    Task ProcessAsync(IncomingMessageContext incomingMessage, CancellationToken ct);
}