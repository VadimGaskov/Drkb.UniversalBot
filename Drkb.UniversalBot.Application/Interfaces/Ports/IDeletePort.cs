namespace Drkb.UniversalBot.Application.Interfaces.DataProvider;

public interface IDeletePort<in TEntity>: IPortMarker where TEntity: class
{
    public void Delete(TEntity entity);
}