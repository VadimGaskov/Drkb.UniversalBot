using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using Drkb.UniversalBot.Domain.Entity;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;

public interface IUpdateCategoryDataProvider: IUpdateDataProvider<Category>, IGetByIdDataProvider<Category>
{
    
}