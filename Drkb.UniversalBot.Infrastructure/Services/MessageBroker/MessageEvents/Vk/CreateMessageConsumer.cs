using System.Text.Json;
using Drkb.UniversalBot.Application.Dtos.Messenger;
using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.Messagers;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.Services.MessageBroker.MessageEvents.Vk;

public class CreateMessageConsumer: IConsumer<VkMessageCreatedEvent>
{
    private readonly ILoggerService _logger;
    private readonly IMessageProcessor _textMessageProcessor;
    private readonly IMessageProcessor _callbackMessageProcessor;

    public CreateMessageConsumer(ILoggerService logger,
        [FromKeyedServices("text")] IMessageProcessor textMessageProcessor,
        [FromKeyedServices("callback")] IMessageProcessor callbackMessageProcessor)
    {
        _logger = logger;
        _textMessageProcessor = textMessageProcessor;
        _callbackMessageProcessor = callbackMessageProcessor;
    }

    public async Task Consume(ConsumeContext<VkMessageCreatedEvent> context)
    {
        var @event = context.Message;

        var incomingContext = @event.Type switch
        {
            "message_new" => MapMessageNew(@event.Object.Deserialize<VkMessageNewObject>()),
            "message_event" => MapMessageEvent(@event.Object.Deserialize<VkMessageEventObject>()),
            _ => null
        };

        if (incomingContext is null)
        {
            _logger.LogInformation($"Unhandled VK event type: {@event.Type}");
            return;
        }

        var processor = incomingContext.IsCallback
                            ? _callbackMessageProcessor
                            : _textMessageProcessor;
        
        await processor.ProcessAsync(incomingContext, context.CancellationToken);
    }

    private IncomingMessageContext? MapMessageNew(VkMessageNewObject? obj) =>
        obj?.Message is null
            ? null
            : new IncomingMessageContext
            {
                Messenger = Messenger.VK,
                ConversationId = obj.Message.PeerId,
                SenderId = obj.Message.FromId,
                Text = obj.Message.Text,
            };

    private IncomingMessageContext? MapMessageEvent(VkMessageEventObject? obj) =>
        obj is null
            ? null
            : new IncomingMessageContext
            {
                Messenger = Messenger.VK,
                ConversationId = obj.PeerId,
                SenderId = obj.UserId,
                CallbackEventId = obj.EventId,
                CallbackPayloadId = obj.Payload.Id
            };
}