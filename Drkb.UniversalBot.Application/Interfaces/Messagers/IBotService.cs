using Drkb.UniversalBot.Application.Dtos;
using Drkb.UniversalBot.Application.Dtos.Messenger;

namespace Drkb.UniversalBot.Application.Interfaces.Messagers;

public interface IBotService
{
    Task SendCompositeAsync(SendMessageContext message, CancellationToken cancellationToken = default);

    Task SendAnswerMessageEventAsync(AnswerCallbackContext callbackContext,
        CancellationToken cancellationToken = default);
}