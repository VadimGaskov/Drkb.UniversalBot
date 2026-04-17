namespace Drkb.UniversalBot.Application.Interfaces.DataProvider;

public interface IUnitOfWork: IPortMarker
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}