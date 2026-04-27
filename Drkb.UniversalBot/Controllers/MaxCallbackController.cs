using Drkb.UniversalBot.Application.Dtos.Messenger.Max.CallbackDtos;
using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.UseCase.Command.Max.InitWebhook;
using Drkb.UniversalBot.Application.UseCase.Command.Max.ResponseMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Drkb.UniversalBot.Controllers;

[ApiController]
[Route("api/max")]
public class MaxCallbackController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILoggerService _logger;
    public MaxCallbackController(IMediator mediator, ILoggerService logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("init")]
    public async Task<ActionResult> InitWebHook(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new InitWebhookCommand(), cancellationToken);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.ErrorMessage);

        return Ok();
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Handle(MaxCallbackRequest request, CancellationToken cancellationToken)
    {
        var maxMessageEvent = new MaxMessageCreatedEvent()
        {
            Message = request.Message,
            Timestamp = request.Timestamp,
            UpdateType = request.UpdateType,
        };
        
        var result = await _mediator.Send(new MaxCallbackCommand(maxMessageEvent), cancellationToken);
        
        if (!result.IsSuccess)
            _logger.LogWarning($"Failed to process webhook message: {result.ErrorMessage}");
        
        return Ok();
    }
}