using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.CreateCategory;

public interface ICreateCategoryDataProvider: IAddDataProvider<Category>, IGetByIdDataProvider<Category?>
{
    
}