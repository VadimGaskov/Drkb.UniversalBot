namespace Drkb.UniversalBot.Application.Interfaces.Ports;

public interface IUnitOfWork: IPortMarker
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}