using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases;
using Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.CreateCategory;
using Drkb.UniversalBot.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.DataProviders.Categories;

public class EFCreateCategoryDataProvider: ICreateCategoryDataProvider
{
    private readonly BotDbContext _context;

    public EFCreateCategoryDataProvider(BotDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Category entity, CancellationToken cancellationToken = default)
    {
        await _context.Categories.AddAsync(entity, cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Categories.FirstOrDefaultAsync(x=>x.Id == id, cancellationToken);
    }
}