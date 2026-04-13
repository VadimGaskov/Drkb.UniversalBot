namespace Drkb.UniversalBot.Application.Interfaces.DataProvider;

public interface IUpdateDataProvider<in TEntity>: IDataProviderMarker where TEntity: class
{
    public void Update(TEntity entity);
}