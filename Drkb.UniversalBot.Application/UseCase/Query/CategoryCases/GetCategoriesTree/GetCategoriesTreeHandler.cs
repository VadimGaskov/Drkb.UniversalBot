using Drkb.ResultObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategoriesTree;

public class GetCategoriesTreeHandler: IRequestHandler<GetCategoriesTreeQuery, Result<List<CategoriesTreeDto>>>
{
    private readonly ICategoriesTreeQuery _categoriesTreeQuery;
    private readonly ILogger<GetCategoriesTreeHandler> _logger;

    public GetCategoriesTreeHandler(ICategoriesTreeQuery categoriesTreeQuery, ILogger<GetCategoriesTreeHandler> logger)
    {
        _categoriesTreeQuery = categoriesTreeQuery;
        _logger = logger;
    }

    public async Task<Result<List<CategoriesTreeDto>>> Handle(GetCategoriesTreeQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрошено получение дерева категорий");
        var result = await _categoriesTreeQuery.ExecuteAsync(request, cancellationToken).ConfigureAwait(false);
        _logger.LogInformation("Дерево категорий успешно получено. КоличествоКорневыхУзлов: {RootNodesCount}", result.Count);
        return Result<List<CategoriesTreeDto>>.Success(result);
    }
}