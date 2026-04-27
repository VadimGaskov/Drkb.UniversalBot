using Drkb.ResultObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategories;

public class GetCategoriesHandler: IRequestHandler<GetCategoriesQuery, Result<List<GetCategoriesDto>>>
{
    private readonly ICategoriesQuery _categoriesQuery;
    private readonly ILogger<GetCategoriesHandler> _logger;

    public GetCategoriesHandler(ICategoriesQuery categoriesQuery, ILogger<GetCategoriesHandler> logger)
    {
        _categoriesQuery = categoriesQuery;
        _logger = logger;
    }

    public async Task<Result<List<GetCategoriesDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрошено получение списка категорий");
        var result = await _categoriesQuery.ExecuteAsync(request, cancellationToken).ConfigureAwait(false);
        _logger.LogInformation("Список категорий получен. КоличествоКатегорий: {CategoriesCount}", result.Count);
        return Result<List<GetCategoriesDto>>.Success(result);
    }
}