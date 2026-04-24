using Drkb.UniversalBot.Application.Dtos.Messenger;

namespace Drkb.UniversalBot.Application.Interfaces.Messagers;

public interface IMessagePostProcessor
{
    Task ProcessAsync(IncomingMessageContext messageContext, CancellationToken cancellationToken);
}