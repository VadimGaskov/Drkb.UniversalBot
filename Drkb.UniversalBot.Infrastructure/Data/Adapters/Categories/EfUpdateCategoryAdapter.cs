using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;
using Drkb.UniversalBot.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.Adapters.Categories;

public class EfUpdateCategoryAdapter: IUpdateCategoryPort
{
    private readonly BotDbContext _context;

    public EfUpdateCategoryAdapter(BotDbContext context)
    {
        _context = context;
    }

    public void Update(Category entity)
    {
        _context.Categories.Update(entity);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Categories.FirstOrDefaultAsync(x=>x.Id == id, cancellationToken);
    }
}