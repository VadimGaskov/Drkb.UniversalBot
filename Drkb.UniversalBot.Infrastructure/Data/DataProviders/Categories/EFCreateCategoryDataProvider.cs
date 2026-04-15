using Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;
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

    public async Task AddCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        await _context.Categories.AddAsync(category, cancellationToken);
    }

    public async Task AddMessageStructuresDataAsync(List<MessageStructure> data, CancellationToken cancellationToken)
    {
        await _context.MessageStructures.AddRangeAsync(data, cancellationToken);
    }
    
    public async Task<Category?> GetCategoryByIdAsync(Guid? categoryId, CancellationToken ct)
    {
        return await _context.Categories.FirstOrDefaultAsync(x=>x.Id == categoryId, ct);
    }
}