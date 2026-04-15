using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;
using Drkb.UniversalBot.Application.UseCase.Command.Statistics.CreateStatistics;
using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;

namespace Drkb.UniversalBot.Infrastructure.Services.VkIntegration;

public class VkMessageNewProcessor: IVkMessageNewProcessor
{
    private readonly ILoggerService _logger;
    private readonly IVkMessageService _vkMessageService;
    private readonly IMediator _mediator;

    public VkMessageNewProcessor(ILoggerService logger, IVkMessageService vkMessageService, IMediator mediator)
    {
        _logger = logger;
        _vkMessageService = vkMessageService;
        _mediator = mediator;
    }

    public async Task ProcessAsync(VkMessageNewObject? message, CancellationToken cancellationToken)
    {
        if (message?.Message is null)
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
        await _mediator.Publish(new CreateStatisticsEvent
        {
            UserId = message.Message.PeerId.ToString(),
            Messenger = Messenger.VK,
        }, cancellationToken);
    }
}