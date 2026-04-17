namespace Drkb.UniversalBot.Application.Interfaces.DataProvider;

public interface IAddPort<in TEntity>: IPortMarker where TEntity: class
{
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}