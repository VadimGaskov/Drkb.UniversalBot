using Drkb.CacheService.Abstractions.Interfaces;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.ResponesDtos;
using Drkb.UniversalBot.Application.UseCase.Command.Statistics.CreateStatistics;
using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;
using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;

namespace Drkb.UniversalBot.Infrastructure.Services.VkIntegration;

public class VkMessageEventProcessor: IVkMessageEventProcessor
{
    private readonly ILoggerService _logger;
    private readonly ICacheService _cacheService;
    private readonly IVkMessageService _vkMessageService;
    private readonly IMediator _mediator;

    public VkMessageEventProcessor(ILoggerService logger, ICacheService cacheService, IVkMessageService vkMessageService, IMediator mediator)
    {
        _logger = logger;
        _cacheService = cacheService;
        _vkMessageService = vkMessageService;
        _mediator = mediator;
    }

    public async Task ProcessAsync(VkMessageEventObject? messageEvent, CancellationToken cancellationToken)
    {
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
        
        var senderKeyboard = await _mediator.Send(new GetMainKeyboardQuery(), cancellationToken);
        
        if (messageEvent.Payload.Command == "back")
        {
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
            Value = data.Value,
            PeerId = messageEvent.PeerId,
            KeyboardPayload = data.Keyboard,
            Name = data.Name,
            FilesUrl = data.FilesUrl,
        };
        
        await _vkMessageService.SendCompositeAsync(messageEvent.PeerId, messageRequest, cancellationToken);
        await _vkMessageService.SendKeyboardAsync(messageEvent.PeerId, "Главное меню:", senderKeyboard, cancellationToken);
        
        await _mediator.Publish(new CreateStatisticsEvent
        {
            UserId = messageEvent.PeerId.ToString(),
            Messenger = Messenger.VK,
            CategortyId = messageEvent.Payload.Id
        }, cancellationToken);
    }
}