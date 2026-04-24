using Drkb.UniversalBot.Application.Interfaces.Ports;
using Drkb.UniversalBot.Domain.Entity;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;

namespace Drkb.UniversalBot.Application.UseCase.Command.Statistics.CreateStatistics;

public interface ICreateStatisticsPort: IPortMarker
{
    Task<SenderUser> CreateUserAsync(string userId, Messenger userMessenger);
    Task CreateStatisticsAsync(SenderUser user, string categoryId);
    Task CreateStatisticsAsync(SenderUser user);
}