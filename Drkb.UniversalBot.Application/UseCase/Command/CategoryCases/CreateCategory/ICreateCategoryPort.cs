using Drkb.UniversalBot.Application.Interfaces.Ports;
using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;

public interface ICreateCategoryPort: IPortMarker
{
    Task AddCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<Category?> GetCategoryByIdAsync(Guid? categoryId, CancellationToken ct);
    Task<int> GetLastSeq(CancellationToken ct);
}