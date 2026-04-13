using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases;
using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.CreateCategory;
using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;
using Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategories;
using Drkb.UniversalBot.Application.UseCase.Query.MessagesStructure.GetMessageStructure;
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

    [HttpGet]
    public async Task<ActionResult<List<CategoriesDto>>> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCategoriesQuery(), cancellationToken);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.ErrorMessage);

        return result.Data;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.ErrorMessage);

        return Ok();
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