using Drkb.UniversalBot.Application.Dtos;
using Drkb.UniversalBot.Application.Dtos.Messenger;
using Drkb.UniversalBot.Application.Interfaces.Messagers;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.Messengers;

public class CallbackMessageProcessor: IMessageProcessor
{
    private readonly IMediator _mediator;
    private readonly IBotService _vkBotService;
    private readonly IMessagePostProcessor _messagePostProcessor;

    public CallbackMessageProcessor(IMediator mediator, 
        [FromKeyedServices(Messenger.VK)]IBotService vkBotService, 
        IMessagePostProcessor messagePostProcessor)
    {
        _mediator = mediator;
        _vkBotService = vkBotService;
        _messagePostProcessor = messagePostProcessor;
    }

    public async Task ProcessAsync(IncomingMessageContext incomingMessage, CancellationToken ct)
    {
        if (!incomingMessage.IsCallback)
            throw new ArgumentException("Message is not a callback");
        
        var callbackContext = new AnswerCallbackContext()
        {
            ConversationId = incomingMessage.ConversationId,
            SenderId = incomingMessage.SenderId,
            EventId = incomingMessage.CallbackEventId,
        };
        await _vkBotService.SendAnswerMessageEventAsync(callbackContext, ct);
        
        if (!Guid.TryParse(incomingMessage.CallbackPayloadId, out var payloadId))
            throw new ArgumentException("Invalid CallbackPayloadId format");
        var data = await _mediator.Send(new GetDataCategoryByIdQuery(payloadId), ct);
        if (data is null)
            throw new InvalidOperationException("Data not found");
        
        var message = new SendMessageContext()
        {
            ConversationId = incomingMessage.ConversationId,
            SenderId = incomingMessage.SenderId,
            FilesUrl = data.FilesUrl,
            Keyboard = data.Keyboard,
            Text = data.Value,
            Messenger = incomingMessage.Messenger
        };
        
        await _vkBotService.SendCompositeAsync(message, ct);
        await _messagePostProcessor.ProcessAsync(incomingMessage, ct);
    }
}