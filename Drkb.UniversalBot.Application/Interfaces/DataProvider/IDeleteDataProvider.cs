namespace Drkb.UniversalBot.Application.Interfaces.DataProvider;

public interface IDeleteDataProvider<in TEntity>: IDataProviderMarker where TEntity: class
{
    public void Delete(TEntity entity);
}