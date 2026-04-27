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
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Infrastructure.Services.VkIntegration;

public class VkMessageEventProcessor: IVkMessageEventProcessor
{
    private readonly ILogger<VkMessageEventProcessor> _logger;
    private readonly ICacheService _cacheService;
    private readonly IVkMessageService _vkMessageService;
    private readonly IMediator _mediator;

    public VkMessageEventProcessor(
        ILogger<VkMessageEventProcessor> logger,
        ICacheService cacheService,
        IVkMessageService vkMessageService,
        IMediator mediator)
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
            _logger.LogError("Обработка события нажатия VK отклонена: отсутствуют данные события");
            return;
        }

        _logger.LogInformation(
            "Получено событие нажатия VK. EventId: {EventId}, PeerId: {PeerId}, UserId: {UserId}, CategoryId: {CategoryId}",
            messageEvent.EventId,
            messageEvent.PeerId,
            messageEvent.UserId,
            messageEvent.Payload.Id);
        
        var alreadyProcessed = await _cacheService.ExistsAsync(messageEvent.EventId);
        if (alreadyProcessed)
        {
            _logger.LogInformation("Повторное событие VK пропущено. EventId: {EventId}", messageEvent.EventId);
            return; 
        }
        await _cacheService.SetAsync(
            messageEvent.EventId, 
            "processed", 
            TimeSpan.FromMinutes(1)
        );
        _logger.LogInformation("Событие VK помечено как обрабатываемое в кеше. EventId: {EventId}", messageEvent.EventId);
        
        try
        {
            await _vkMessageService.SendAnswerMessageEventAsync(messageEvent.EventId, messageEvent.UserId, messageEvent.PeerId, cancellationToken);
            _logger.LogInformation("Ответ на событие VK успешно отправлен. EventId: {EventId}, PeerId: {PeerId}",
                messageEvent.EventId,
                messageEvent.PeerId);
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Не удалось отправить подтверждение события VK. EventId: {EventId}, PeerId: {PeerId}, Причина: {Reason}",
                messageEvent.EventId,
                messageEvent.PeerId,
                e.Message);
        }
        
        var data = await _mediator.Send(new GetDataCategoryByIdQuery(Guid.Parse(messageEvent.Payload.Id)), cancellationToken);
        if (data is null)
        {
            _logger.LogError("Обработка события VK завершена с ошибкой: не найдены данные категории. EventId: {EventId}, CategoryId: {CategoryId}",
                messageEvent.EventId,
                messageEvent.Payload.Id);
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
        _logger.LogInformation("Составное сообщение по категории отправлено. EventId: {EventId}, PeerId: {PeerId}, CategoryId: {CategoryId}",
            messageEvent.EventId,
            messageEvent.PeerId,
            messageEvent.Payload.Id);
        
        var senderKeyboard = await _mediator.Send(new GetMainKeyboardQuery(), cancellationToken);
        await _vkMessageService.SendKeyboardAsync(messageEvent.PeerId, "Главное меню:", senderKeyboard, cancellationToken);
        _logger.LogInformation("Главная клавиатура отправлена после обработки события. EventId: {EventId}, PeerId: {PeerId}",
            messageEvent.EventId,
            messageEvent.PeerId);
        
        await _mediator.Publish(new CreateStatisticsEvent
        {
            UserId = messageEvent.PeerId.ToString(),
            Messenger = Messenger.VK,
            CategortyId = messageEvent.Payload.Id
        }, cancellationToken);
        _logger.LogInformation("Статистика события VK сохранена. EventId: {EventId}, PeerId: {PeerId}, CategoryId: {CategoryId}",
            messageEvent.EventId,
            messageEvent.PeerId,
            messageEvent.Payload.Id);
    }
}