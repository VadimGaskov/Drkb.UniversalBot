namespace Drkb.UniversalBot.Application.Interfaces.Ports;

public interface IGetByIdPort<TResponse>: IPortMarker
{
    public Task<TResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}