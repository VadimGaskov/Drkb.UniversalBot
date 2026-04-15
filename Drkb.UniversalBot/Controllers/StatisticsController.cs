using Drkb.UniversalBot.Application.UseCase.Query.Statistics.GetDashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Drkb.UniversalBot.Controllers;

[ApiController]
[Route("api/statistics")]
public class StatisticsController: ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<GetDashboardDto>> GetDashboard(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDashboardRequest(), cancellationToken);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.ErrorMessage);

        return result.Data;
    }
}