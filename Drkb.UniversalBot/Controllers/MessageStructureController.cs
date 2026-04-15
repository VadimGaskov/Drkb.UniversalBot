using Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;
using Drkb.UniversalBot.Application.UseCase.Query.MessagesStructure.GetMessageStructure;
using Drkb.UniversalBot.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Drkb.UniversalBot.Controllers;

[ApiController]
[Route("api/structure-of-message")]
public class MessageStructureController: ControllerBase
{
    private readonly IMediator _mediator;

    public MessageStructureController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
}