using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategories;

public class GetCategoriesHandler: IRequestHandler<GetCategoriesQuery, Result<List<CategoriesDto>>>
{
    private readonly ICategoriesQuery _categoriesQuery;

    public GetCategoriesHandler(ICategoriesQuery categoriesQuery)
    {
        _categoriesQuery = categoriesQuery;
    }

    public async Task<Result<List<CategoriesDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _categoriesQuery.ExecuteAsync(request, cancellationToken);
        return Result<List<CategoriesDto>>.Success(result);
    }
}