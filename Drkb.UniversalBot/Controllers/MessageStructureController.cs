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
    

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> Create([FromForm] CreateMessageStructureRequest request, CancellationToken cancellationToken)
    {
        var payloads = new List<CreateMessageStructurePayload>();
        foreach (var item in request.Items)
        {
            AppFile? appFile = null;

            if (item.File is not null)
            {
                await using var stream = item.File.OpenReadStream();
                using var memoryStream = new MemoryStream();

                await stream.CopyToAsync(memoryStream, cancellationToken);

                appFile = new AppFile
                {
                    FileName = item.File.FileName,
                    ContentType = item.File.ContentType,
                    Content = memoryStream.ToArray(),
                    Length = item.File.Length
                };
            }

            payloads.Add(new CreateMessageStructurePayload
            {
                Title = item.Title,
                Value = item.Value,
                Seq = item.Seq,
                TypeField = item.TypeField,
                File = appFile
            });
        }

        var command = new CreateMessageStructureCommand
        {
            CategoryId = request.CategoryId,
            Payloads = payloads
        };

        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}