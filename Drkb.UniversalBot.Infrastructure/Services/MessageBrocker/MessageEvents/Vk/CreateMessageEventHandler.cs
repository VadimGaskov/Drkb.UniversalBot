using System.Text.Json;
using Drkb.CacheService.Abstractions.Interfaces;
using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;
using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;
using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Consumer;

namespace Drkb.UniversalBot.Infrastructure.Services.MessageBrocker.MessageEvents.Vk;

public class CreateMessageEventHandler: IEventHandler<VkMessageEvent>
{
    private readonly ILoggerService _logger;
    private readonly IVkMessageEventProcessor _vkMessageEventProcessor;
    private readonly IVkMessageNewProcessor _vkMessageNewProcessor;

    public CreateMessageEventHandler(ILoggerService logger, IVkMessageEventProcessor vkMessageEventProcessor, IVkMessageNewProcessor vkMessageNewProcessor)
    {
        _logger = logger;
        _vkMessageEventProcessor = vkMessageEventProcessor;
        _vkMessageNewProcessor = vkMessageNewProcessor;
    }

    public async Task HandleAsync(VkMessageEvent @event, CancellationToken cancellationToken = default)
    {
        
        switch (@event.Type)
        {
            case "message_new":
                var message = @event.Object.Deserialize<VkMessageNewObject>();
                await _vkMessageNewProcessor.ProcessAsync(message, cancellationToken);
                return;
            case "message_event":
                var messageEvent = @event.Object.Deserialize<VkMessageEventObject>();
                await _vkMessageEventProcessor.ProcessAsync(messageEvent, cancellationToken);
                return;
            default:
                _logger.LogInformation($"Unhandled VK event type: {@event.Type}");
                return;
        }
    }
}