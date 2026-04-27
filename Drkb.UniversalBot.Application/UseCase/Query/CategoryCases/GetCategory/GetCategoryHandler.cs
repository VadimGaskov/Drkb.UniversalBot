using Drkb.ResultObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategory;

public class GetCategoryHandler: IRequestHandler<GetCategoryQuery, Result<GetCategoryDto>>
{
    private readonly ICategoryQuery _categoryQuery;
    private readonly ILogger<GetCategoryHandler> _logger;

    public GetCategoryHandler(ICategoryQuery categoryQuery, ILogger<GetCategoryHandler> logger)
    {
        _categoryQuery = categoryQuery;
        _logger = logger;
    }

    public async Task<Result<GetCategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрошена категория. CategoryId: {CategoryId}", request.Id);
        var result = await _categoryQuery.ExecuteAsync(request, cancellationToken);
        
        if (result is null)
        {
            _logger.LogError("Категория не найдена. CategoryId: {CategoryId}", request.Id);
            return Result<GetCategoryDto>.NotFound("Category not found");
        }
        
        _logger.LogInformation("Категория успешно получена. CategoryId: {CategoryId}", request.Id);
        return Result<GetCategoryDto>.Success(result);
    }
}