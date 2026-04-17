using Drkb.UniversalBot.Application.Interfaces.DataProvider;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.DeleteCategory;

public interface IDeleteCategoryPort: IPortMarker
{
    Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<bool> CheckExistsAsync(Guid categoryId, CancellationToken cancellationToken);
}