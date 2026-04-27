using Drkb.UniversalBot.Application.Dtos;
using Drkb.UniversalBot.Application.Dtos.Messenger;
using Drkb.UniversalBot.Application.Interfaces.Messagers;

namespace Drkb.UniversalBot.Infrastructure.Messengers.Max;

public class MaxMessageService: IBotService
{
    public Task SendCompositeAsync(SendMessageContext message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SendAnswerMessageEventAsync(AnswerCallbackContext callbackContext, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}