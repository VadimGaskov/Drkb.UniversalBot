using Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;

namespace Drkb.UniversalBot.Application.Interfaces.VkIntegration;

public interface IVkMessageService
{
    Task SendTextAsync(long peerId, string text, CancellationToken cancellationToken = default);
    Task SendKeyboardAsync(long peerId, string text, string keyboardPayload, CancellationToken cancellationToken = default);
    
    Task SendCompositeAsync(long peerId, VkSendMessageRequest message, CancellationToken cancellationToken = default);

    Task SendAnswerMessageEventAsync(string eventId, long userId, long peerId,
        CancellationToken cancellationToken = default);
}