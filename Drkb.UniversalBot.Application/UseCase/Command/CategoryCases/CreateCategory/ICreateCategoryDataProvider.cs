using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;

public interface ICreateCategoryDataProvider: IDataProviderMarker
{
    Task AddCategoryAsync(Category category, CancellationToken cancellationToken);
    Task AddMessageStructuresDataAsync(List<MessageStructure> data, CancellationToken cancellationToken);
    Task<Category?> GetCategoryByIdAsync(Guid? categoryId, CancellationToken ct);
}