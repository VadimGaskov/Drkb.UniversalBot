using Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategory;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.QueryObjects.Categories;

public class EfCategoryQuery: ICategoryQuery
{
    private readonly BotDbContext _context;

    public EfCategoryQuery(BotDbContext context)
    {
        _context = context;
    }

    public async Task<GetCategoryDto?> ExecuteAsync(GetCategoryQuery query, CancellationToken cancellationToken = default)
    {
        return await _context.Categories.Where(x => x.Id == query.Id)
            .Select(x => new GetCategoryDto
            {
                Id = x.Id,
                Name = x.Title,
                Value = x.Value,
                ParentCategoryId = x.ParentCategoryId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}