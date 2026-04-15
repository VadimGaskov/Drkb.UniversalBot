using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.Statistics.GetDashboard;

public record GetDashboardRequest() : IRequest<Result<GetDashboardDto>>;
