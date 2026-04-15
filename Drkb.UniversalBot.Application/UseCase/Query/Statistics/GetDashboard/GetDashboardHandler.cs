using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.Statistics.GetDashboard;

public class GetDashboardHandler: IRequestHandler<GetDashboardRequest, Result<GetDashboardDto>>
{
    private readonly IDashboardQuery _dashboardQuery;

    public GetDashboardHandler(IDashboardQuery dashboardQuery)
    {
        _dashboardQuery = dashboardQuery;
    }

    public async Task<Result<GetDashboardDto>> Handle(GetDashboardRequest request, CancellationToken cancellationToken)
    {
        var result = await _dashboardQuery.ExecuteAsync(cancellationToken);
        return Result<GetDashboardDto>.Success(result);
    }
}