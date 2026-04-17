namespace Drkb.UniversalBot.Application.Interfaces.DataProvider;

public interface IGetByIdPort<TResponse>: IPortMarker
{
    public Task<TResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}