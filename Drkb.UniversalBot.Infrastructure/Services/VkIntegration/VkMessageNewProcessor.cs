using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;
using Drkb.UniversalBot.Application.UseCase.Command.Statistics.CreateStatistics;
using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Infrastructure.Services.VkIntegration;

public class VkMessageNewProcessor: IVkMessageNewProcessor
{
    private readonly ILogger<VkMessageNewProcessor> _logger;
    private readonly IVkMessageService _vkMessageService;
    private readonly IMediator _mediator;

    public VkMessageNewProcessor(ILogger<VkMessageNewProcessor> logger, IVkMessageService vkMessageService, IMediator mediator)
    {
        _logger = logger;
        _vkMessageService = vkMessageService;
        _mediator = mediator;
    }

    public async Task ProcessAsync(VkMessageNewObject? message, CancellationToken cancellationToken)
    {
        if (message?.Message is null)
        {
            _logger.LogError("Обработка входящего сообщения VK отклонена: отсутствуют данные сообщения");
            return;
        }

        _logger.LogInformation(
            "Получено входящее сообщение VK. PeerId: {PeerId}, Text: {Text}",
            message.Message.PeerId,
            message.Message.Text);

        switch (message.Message.Text)
        {
            case "Начать":
                _logger.LogInformation("Отправляется приветственное сообщение пользователю. PeerId: {PeerId}", message.Message.PeerId);
                await _vkMessageService.SendTextAsync(message.Message.PeerId, "Привет", cancellationToken);
                break;
            default:
                _logger.LogInformation("Отправляется сообщение о непонятной команде. PeerId: {PeerId}, Text: {Text}",
                    message.Message.PeerId,
                    message.Message.Text);
                await _vkMessageService.SendTextAsync(message.Message.PeerId, "Извините я вас не понял", cancellationToken);
                break;
        }

        var mainKeyboard = await _mediator.Send(new GetMainKeyboardQuery(), cancellationToken);
        await _vkMessageService.SendKeyboardAsync(message.Message.PeerId, "Выберите вариант из клавиатуры", mainKeyboard, cancellationToken);
        _logger.LogInformation("Главная клавиатура отправлена пользователю. PeerId: {PeerId}", message.Message.PeerId);

        await _mediator.Publish(new CreateStatisticsEvent
        {
            UserId = message.Message.PeerId.ToString(),
            Messenger = Messenger.VK,
        }, cancellationToken);
        _logger.LogInformation("Статистика входящего сообщения сохранена. PeerId: {PeerId}", message.Message.PeerId);
    }
}