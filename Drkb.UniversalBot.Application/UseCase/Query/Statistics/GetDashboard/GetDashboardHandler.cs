using Drkb.ResultObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Query.Statistics.GetDashboard;

public class GetDashboardHandler: IRequestHandler<GetDashboardRequest, Result<GetDashboardDto>>
{
    private readonly IDashboardQuery _dashboardQuery;
    private readonly ILogger<GetDashboardHandler> _logger;

    public GetDashboardHandler(IDashboardQuery dashboardQuery, ILogger<GetDashboardHandler> logger)
    {
        _dashboardQuery = dashboardQuery;
        _logger = logger;
    }

    public async Task<Result<GetDashboardDto>> Handle(GetDashboardRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрошено формирование дашборда статистики");
        var result = await _dashboardQuery.ExecuteAsync(cancellationToken);
        _logger.LogInformation("Дашборд статистики успешно сформирован");
        return Result<GetDashboardDto>.Success(result);
    }
}