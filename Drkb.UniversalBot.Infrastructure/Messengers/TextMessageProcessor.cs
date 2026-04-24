using Drkb.UniversalBot.Application.Dtos.Messenger;
using Drkb.UniversalBot.Application.Interfaces.Messagers;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.Messengers;

public class TextMessageProcessor: IMessageProcessor
{
    private readonly IBotService _vkBotService;
    private readonly IMessagePostProcessor _messagePostProcessor;

    public TextMessageProcessor(IMessagePostProcessor messagePostProcessor, 
        [FromKeyedServices(Messenger.VK)]IBotService vkBotService)
    {
        _vkBotService = vkBotService;
        _messagePostProcessor = messagePostProcessor;
    }

    public async Task ProcessAsync(IncomingMessageContext incomingMessage, CancellationToken ct)
    {
        var sendMessageContext = new SendMessageContext
        {
            ConversationId = incomingMessage.ConversationId,
            SenderId = incomingMessage.SenderId,
            Messenger = incomingMessage.Messenger,
            Text = incomingMessage.Text switch
            {
                "Начать" => "Привет",
                _ => "Извините я вас не понял"
            }
        };
        await _vkBotService.SendCompositeAsync(sendMessageContext, ct);
        await _messagePostProcessor.ProcessAsync(incomingMessage, ct);
    }
}