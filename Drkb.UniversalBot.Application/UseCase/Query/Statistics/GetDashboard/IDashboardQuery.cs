using Drkb.UniversalBot.Application.Interfaces.QueryObjects;

namespace Drkb.UniversalBot.Application.UseCase.Query.Statistics.GetDashboard;

public interface IDashboardQuery: IQueryMarker
{ 
    Task<GetDashboardDto> ExecuteAsync(CancellationToken cancellationToken);
}