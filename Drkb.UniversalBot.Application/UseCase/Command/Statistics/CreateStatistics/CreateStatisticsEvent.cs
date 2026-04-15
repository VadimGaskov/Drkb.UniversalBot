using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.Statistics.CreateStatistics;

public record CreateStatisticsEvent: INotification
{
    public string UserId { get; set; }
    public string? CategortyId { get; set; }
    public Messenger Messenger { get; set; }
}