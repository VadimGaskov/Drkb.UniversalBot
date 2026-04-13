using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;

public interface ICreateStructureOfMessageDataProvider: IAddDataProvider<IEnumerable<MessageStructure>>
{
    Task<Category?> GetCategoryByIdAsync(Guid categoryId, CancellationToken ct);
}