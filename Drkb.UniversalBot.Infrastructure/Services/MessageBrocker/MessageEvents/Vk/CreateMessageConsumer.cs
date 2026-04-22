using System.Text.Json;
using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;
using MassTransit;

namespace Drkb.UniversalBot.Infrastructure.Services.MessageBrocker.MessageEvents.Vk;

public class CreateMessageConsumer: IConsumer<VkMessageCreatedEvent>
{
    private readonly ILoggerService _logger;
    private readonly IVkMessageEventProcessor _vkMessageEventProcessor;
    private readonly IVkMessageNewProcessor _vkMessageNewProcessor;

    public CreateMessageConsumer(ILoggerService logger, IVkMessageEventProcessor vkMessageEventProcessor, IVkMessageNewProcessor vkMessageNewProcessor)
    {
        _logger = logger;
        _vkMessageEventProcessor = vkMessageEventProcessor;
        _vkMessageNewProcessor = vkMessageNewProcessor;
    }

    public async Task Consume(ConsumeContext<VkMessageCreatedEvent> context)
    {
        var @event = context.Message;
        switch (@event.Type)
        {
            case "message_new":
                var message = @event.Object.Deserialize<VkMessageNewObject>();
                await _vkMessageNewProcessor.ProcessAsync(message, CancellationToken.None);
                return;
            case "message_event":
                var messageEvent = @event.Object.Deserialize<VkMessageEventObject>();
                await _vkMessageEventProcessor.ProcessAsync(messageEvent, CancellationToken.None);
                return;
            default:
                _logger.LogInformation($"Unhandled VK event type: {@event.Type}");
                return;
        }
    }
}