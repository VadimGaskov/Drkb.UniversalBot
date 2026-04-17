using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.DeleteCategory;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.Adapters.Categories;

public class EfDeleteCategoryPort: IDeleteCategoryPort
{
    private readonly BotDbContext _context;

    public EfDeleteCategoryPort(BotDbContext context)
    {
        _context = context;
    }

    public async Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
        
        if(category == null) 
            throw new InvalidOperationException($"Category with id {categoryId} not found");

        if (category.CategoryStatus == CategoryStatus.InActive)
            return;
        
        await _context.Categories
            .Where(x => x.ParentCategoryId == category.Id)
            .ExecuteUpdateAsync(
                x => x.SetProperty(p => p.ParentCategoryId, category.ParentCategoryId),
                cancellationToken);

        category.CategoryStatus = CategoryStatus.InActive;
    }

    public async Task<bool> CheckExistsAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context.Categories.AnyAsync(x=>x.Id == categoryId, cancellationToken);
    }
}