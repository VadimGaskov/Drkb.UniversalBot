namespace Drkb.UniversalBot.Application.Interfaces.DataProvider;

public interface IUnitOfWork: IDataProviderMarker
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}