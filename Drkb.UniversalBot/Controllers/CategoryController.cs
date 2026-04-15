using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases;
using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.CreateCategory;
using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;
using Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;
using Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategories;
using Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategoriesTree;
using Drkb.UniversalBot.Application.UseCase.Query.MessagesStructure.GetMessageStructure;
using Drkb.UniversalBot.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Drkb.UniversalBot.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController: ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("tree")]
    public async Task<ActionResult<List<CategoriesTreeDto>>> GetTree(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCategoriesTreeQuery(), cancellationToken);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.ErrorMessage);

        return result.Data;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GetCategoriesDto>>> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCategoriesQuery(), cancellationToken);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.ErrorMessage);

        return result.Data;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> Create([FromForm] CreateCategoryRequest request, CancellationToken cancellationToken)
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
                Name = item.Name,
                Value = item.Value,
                Seq = item.Seq,
                TypeField = item.TypeField,
                File = appFile
            });
        }

        var command = new CreateCategoryCommand
        {
            NameCategory = request.NameCategory,
            ParentCategoryId = request.ParentCategoryId,
            Payloads = payloads
        };

        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, UpdateCategoryCommand command, CancellationToken token)
    {
        if (command.Id != id)
            return BadRequest("Ids do not match");
        
        var result = await _mediator.Send(command, token);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.ErrorMessage);

        return Ok();
    }

    [HttpGet("{id:guid}/message-structure")]
    public async Task<ActionResult<GetMessagesStructureDto>> GetMessageStructure(Guid id,
        CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            return BadRequest("Id is empty");
        
        var result = await _mediator.Send(new GetMessagesStructureQuery(id), cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.ErrorMessage);

        return result.Data;
    }
}