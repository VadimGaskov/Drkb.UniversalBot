using Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;
using Drkb.UniversalBot.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.Adapters.Categories;

public class EfCreateCategoryAdapter: ICreateCategoryPort
{
    private readonly BotDbContext _context;

    public EfCreateCategoryAdapter(BotDbContext context)
    {
        _context = context;
    }

    public async Task AddCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        await _context.Categories.AddAsync(category, cancellationToken);
    }
    
    public async Task<Category?> GetCategoryByIdAsync(Guid? categoryId, CancellationToken ct)
    {
        return await _context.Categories.FirstOrDefaultAsync(x=>x.Id == categoryId, ct);
    }
}