using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateSeq;
using Drkb.UniversalBot.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.Adapters.Categories;

public class EfReorderCategoryAdapter: IReorderCategoryPort
{
    private readonly BotDbContext _context;

    public EfReorderCategoryAdapter(BotDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategoriesAsync(IEnumerable<Guid> categoriesIds, CancellationToken cancellationToken)
    {
        return await _context.Categories.Where(x => categoriesIds.Contains(x.Id)).ToListAsync(cancellationToken);
    }
}