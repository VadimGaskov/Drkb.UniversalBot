using Drkb.UniversalBot.Application.Interfaces.Ports;
using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateSeq;

public interface IReorderCategoryPort: IPortMarker
{
    Task<List<Category>> GetCategoriesAsync(IEnumerable<Guid> categoriesIds, CancellationToken cancellationToken);
}