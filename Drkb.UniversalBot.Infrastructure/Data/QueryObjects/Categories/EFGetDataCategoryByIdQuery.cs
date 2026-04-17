using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.QueryObjects.Categories;

public class EFGetDataCategoryByIdQuery: IGetDataCategoryByIdQuery
{
    private readonly BotDbContext _context;

    public EFGetDataCategoryByIdQuery(BotDbContext context)
    {
        _context = context;
    }

    public async Task<GetDataCategoryByIdDto?> ExecuteAsync(GetDataCategoryByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Where(x=>x.Id == query.CategoryId && x.CategoryStatus == CategoryStatus.Active)
            .Select(x=>new GetDataCategoryByIdDto
            {
                Categories = x.ChildrenCategories.Where(cc => cc.CategoryStatus == CategoryStatus.Active).ToList(),
                CategoryId = x.Id,
                Text = x.Title,
                Value = x.Value
            }).FirstOrDefaultAsync(cancellationToken);
    }
}