namespace Drkb.UniversalBot.Application.Interfaces.DataProvider;

public interface IAddDataProvider<in TEntity>: IDataProviderMarker where TEntity: class
{
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}