using Drkb.UniversalBot.Application.Dtos.Messenger;
using Drkb.UniversalBot.Application.Interfaces.Messagers;
using Drkb.UniversalBot.Application.UseCase.Command.Statistics.CreateStatistics;
using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Drkb.UniversalBot.Infrastructure.Messengers;

public class MessagePostProcessor: IMessagePostProcessor
{
    private readonly IMediator _mediator;
    private readonly IBotService _vkBotService;

    public MessagePostProcessor(IMediator mediator, 
        [FromKeyedServices(Messenger.VK)]IBotService vkBotService)
    {
        _mediator = mediator;
        _vkBotService = vkBotService;
    }

    public async Task ProcessAsync(IncomingMessageContext messageContext, CancellationToken cancellationToken)
    {
        var mainKeyboard = await _mediator.Send(new GetMainKeyboardQuery(), cancellationToken);

        var mainMenuMessage = new SendMessageContext
        {
            ConversationId = messageContext.ConversationId,
            SenderId = messageContext.SenderId,
            Keyboard = mainKeyboard,
            Text = "Главное меню:",
            Messenger = Messenger.VK,
        };
        await _vkBotService.SendCompositeAsync(mainMenuMessage, cancellationToken);
        
        await _mediator.Publish(new CreateStatisticsEvent
        {
            UserId = messageContext.SenderId.ToString(),
            Messenger = Messenger.VK,
        }, cancellationToken);
    }
}