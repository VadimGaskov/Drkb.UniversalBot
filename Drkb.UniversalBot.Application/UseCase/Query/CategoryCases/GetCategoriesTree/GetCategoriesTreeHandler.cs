using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategoriesTree;

public class GetCategoriesTreeHandler: IRequestHandler<GetCategoriesTreeQuery, Result<List<CategoriesTreeDto>>>
{
    private readonly ICategoriesTreeQuery _categoriesTreeQuery;

    public GetCategoriesTreeHandler(ICategoriesTreeQuery categoriesTreeQuery)
    {
        _categoriesTreeQuery = categoriesTreeQuery;
    }

    public async Task<Result<List<CategoriesTreeDto>>> Handle(GetCategoriesTreeQuery request, CancellationToken cancellationToken)
    {
        var result = await _categoriesTreeQuery.ExecuteAsync(request, cancellationToken).ConfigureAwait(false);
        return Result<List<CategoriesTreeDto>>.Success(result);
    }
}