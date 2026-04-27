using System.Text.Json;
using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Infrastructure.Services.MessageBrocker.MessageEvents.Vk;

public class CreateMessageConsumer: IConsumer<VkMessageCreatedEvent>
{
    private readonly ILogger<CreateMessageConsumer> _logger;
    private readonly IVkMessageEventProcessor _vkMessageEventProcessor;
    private readonly IVkMessageNewProcessor _vkMessageNewProcessor;

    public CreateMessageConsumer(
        ILogger<CreateMessageConsumer> logger,
        IVkMessageEventProcessor vkMessageEventProcessor,
        IVkMessageNewProcessor vkMessageNewProcessor)
    {
        _logger = logger;
        _vkMessageEventProcessor = vkMessageEventProcessor;
        _vkMessageNewProcessor = vkMessageNewProcessor;
    }

    public async Task Consume(ConsumeContext<VkMessageCreatedEvent> context)
    {
        var @event = context.Message;
        _logger.LogInformation("Получено событие VK. ТипСобытия: {EventType}", @event.Type);

        switch (@event.Type)
        {
            case "message_new":
                var message = @event.Object.Deserialize<VkMessageNewObject>();
                if (message is null)
                {
                    _logger.LogError("Обработка события VK отклонена: тело события не удалось преобразовать в message_new. ТипСобытия: {EventType}", @event.Type);
                    return;
                }

                await _vkMessageNewProcessor.ProcessAsync(message, CancellationToken.None);
                _logger.LogInformation("Событие VK message_new успешно обработано. PeerId: {PeerId}", message.Message?.PeerId);
                return;
            case "message_event":
                var messageEvent = @event.Object.Deserialize<VkMessageEventObject>();
                if (messageEvent is null)
                {
                    _logger.LogError("Обработка события VK отклонена: тело события не удалось преобразовать в message_event. ТипСобытия: {EventType}", @event.Type);
                    return;
                }

                await _vkMessageEventProcessor.ProcessAsync(messageEvent, CancellationToken.None);
                _logger.LogInformation("Событие VK message_event успешно обработано. EventId: {EventId}, PeerId: {PeerId}",
                    messageEvent.EventId,
                    messageEvent.PeerId);
                return;
            default:
                _logger.LogError("Событие VK не обработано: неподдерживаемый тип события. ТипСобытия: {EventType}", @event.Type);
                return;
        }
    }
}