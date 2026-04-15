using System.Text.Json;
using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.Options;
using Drkb.UniversalBot.Application.UseCase.Command.VkApi.ResponseMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Drkb.UniversalBot.Controllers;

[ApiController]
[Route("api/vk/callback")]
public class VkCallbackController : ControllerBase
{
    private readonly VkOptions _options;
    private readonly IVkMessageService _vkMessageService;
    private readonly ILoggerService _logger;
    private readonly IMediator _mediator;

    public VkCallbackController(
        IOptions<VkOptions> options,
        IVkMessageService vkMessageService,
        ILoggerService logger, IMediator mediator)
    {
        _options = options.Value;
        _vkMessageService = vkMessageService;
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Handle(
        [FromBody] VkCallbackRequest request,
        CancellationToken cancellationToken)
    {
        if (request.GroupId != _options.GroupId)
        {
            _logger.LogWarning($"Invalid group id: {request.GroupId}");
            return BadRequest("invalid group_id");
        }
        
        if (!string.IsNullOrWhiteSpace(_options.Secret) &&
            !string.Equals(request.Secret, _options.Secret, StringComparison.Ordinal))
        {
            _logger.LogWarning("Invalid callback secret");
            return BadRequest("invalid secret");
        }

        if (request.Type == "confirmation")
            return Content(_options.ConfirmationCode);
        

        var result = await _mediator.Send(new VkResponseMessageCommand(new VkMessageEvent()
        {
            GroupId = _options.GroupId,
            Object = request.Object,
            Secret = request.Secret,
            Type = request.Type,
        }), cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogInformation($"Unhandled VK event type: {request.Type}");
            return Content("ok");
        }
        return Content("ok");

        
    }
}