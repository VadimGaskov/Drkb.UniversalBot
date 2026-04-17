using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategory;

public class GetCategoryHandler: IRequestHandler<GetCategoryQuery, Result<GetCategoryDto>>
{
    private readonly ICategoryQuery _categoryQuery;

    public GetCategoryHandler(ICategoryQuery categoryQuery)
    {
        _categoryQuery = categoryQuery;
    }

    public async Task<Result<GetCategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _categoryQuery.ExecuteAsync(request, cancellationToken);
        
        if (result is null)
            return Result<GetCategoryDto>.NotFound("Category not found");
        
        return Result<GetCategoryDto>.Success(result);
    }
}