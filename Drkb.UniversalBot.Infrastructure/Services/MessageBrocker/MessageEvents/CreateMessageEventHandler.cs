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

namespace Drkb.UniversalBot.Infrastructure.Services.MessageBrocker.MessageEvents;

public class CreateMessageEventHandler: IEventHandler<MessageEvent>
{
    private readonly ILoggerService _logger;
    private readonly IMediator _mediator;
    private readonly IVkMessageService _vkMessageService;
    private readonly ICacheService _cacheService;

    public CreateMessageEventHandler(ILoggerService logger, IMediator mediator, IVkMessageService vkMessageService, ICacheService cacheService)
    {
        _logger = logger;
        _mediator = mediator;
        _vkMessageService = vkMessageService;
        _cacheService = cacheService;
    }

    public async Task HandleAsync(MessageEvent @event, CancellationToken cancellationToken = new CancellationToken())
    {
        
        switch (@event.Type)
        {
            case "message_new":
                var message = @event.Object.Deserialize<VkMessageNewObject>();
                if (message is null)
                {
                    _logger.LogError("Invalid message event");
                    return;
                }
                _logger.LogInformation($"Incoming VK message from {message.Message.PeerId}: {message.Message.Text}");

                switch (message.Message.Text)
                {
                    case "Начать":
                        await _vkMessageService.SendTextAsync(message.Message.PeerId, "Привет", cancellationToken);
                        break;
                    default:
                        await _vkMessageService.SendTextAsync(message.Message.PeerId, "Извините я вас не понял", cancellationToken);
                        break;
                }
                var mainKeyboard = await _mediator.Send(new GetMainKeyboardQuery(), cancellationToken);
                await _vkMessageService.SendKeyboardAsync(message.Message.PeerId, "Выберите вариант из клавиатуры", mainKeyboard, cancellationToken);
                return;
            
            case "message_event":
                var messageEvent = @event.Object.Deserialize<VkMessageEventObject>();
                
                if (messageEvent is null)
                {
                    _logger.LogError("Invalid message event");
                    return;
                }
                
                var alreadyProcessed = await _cacheService.ExistsAsync(messageEvent.EventId);
                if (alreadyProcessed)
                {
                    return; 
                }
                
                await _cacheService.SetAsync(
                    messageEvent.EventId, 
                    "processed", 
                    TimeSpan.FromMinutes(1)
                );
                
                try
                {
                    await _vkMessageService.SendAnswerMessageEventAsync(messageEvent.EventId, messageEvent.UserId, messageEvent.PeerId, cancellationToken);
                }
                catch (Exception e)
                {
                    _logger.LogError("Failed to process message event", e);
                }
                
                
                if (messageEvent.Payload.Command == "back")
                {
                    var senderKeyboard = await _mediator.Send(new GetMainKeyboardQuery(), cancellationToken);
                    await _vkMessageService.SendKeyboardAsync(messageEvent.PeerId, "Выберите вариант из клавиатуры", senderKeyboard, cancellationToken);
                    return;
                }
                
                var data = await _mediator.Send(new GetDataCategoryByIdQuery(Guid.Parse(messageEvent.Payload.Id)), cancellationToken);
                if (data is null)
                {
                    _logger.LogError("Invalid data message event");
                    return;
                }

                var messageRequest = new VkSendMessageRequest()
                {
                    Text = data.Text,
                    PeerId = messageEvent.PeerId,
                    FilePaths = data.Files,
                    KeyboardPayload = data.Keyboard
                };
                
                await _vkMessageService.SendCompositeAsync(messageEvent.PeerId, messageRequest, cancellationToken);
                break;
            default:
                _logger.LogInformation($"Unhandled VK event type: {@event.Type}");
                return;
                
        }
    }
    
}