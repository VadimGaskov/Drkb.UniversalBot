using Drkb.UniversalBot.Application.Interfaces.QueryObjects;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategories;

public interface ICategoriesQuery: IQueryObject<GetCategoriesQuery, List<CategoriesDto>>
{
    
}