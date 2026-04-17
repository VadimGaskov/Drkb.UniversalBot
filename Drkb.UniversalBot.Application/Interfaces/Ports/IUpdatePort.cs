namespace Drkb.UniversalBot.Application.Interfaces.DataProvider;

public interface IUpdatePort<in TEntity>: IPortMarker where TEntity: class
{
    public void Update(TEntity entity);
}