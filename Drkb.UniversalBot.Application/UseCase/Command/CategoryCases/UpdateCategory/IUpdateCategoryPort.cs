using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;

public interface IUpdateCategoryPort: IUpdatePort<Category>, IGetByIdPort<Category>
{
    
}