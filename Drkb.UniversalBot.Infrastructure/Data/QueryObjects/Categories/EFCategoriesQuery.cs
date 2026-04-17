using Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategories;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.QueryObjects.Categories;

public class EFCategoriesQuery: ICategoriesQuery
{
    private readonly BotDbContext _context;

    public EFCategoriesQuery(BotDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetCategoriesDto>> ExecuteAsync(GetCategoriesQuery query, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(x=>x.CategoryStatus == CategoryStatus.Active)
            .OrderBy(x=>x.Seq)
            .Select(x => new GetCategoriesDto
            {
                Id = x.Id,
                Name = x.Title
            })
            .ToListAsync(cancellationToken);
    }
}