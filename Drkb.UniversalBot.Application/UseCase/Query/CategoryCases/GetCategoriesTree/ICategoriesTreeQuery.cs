using Drkb.UniversalBot.Application.Interfaces.QueryObjects;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategoriesTree;

public interface ICategoriesTreeQuery: IQueryObject<GetCategoriesTreeQuery, List<CategoriesTreeDto>>
{
    
}