using Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;
using Drkb.UniversalBot.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.DataProviders.MessagesStructure;

public class EFCreateStructureOfMessageDataProvider: ICreateStructureOfMessageDataProvider
{
    private readonly BotDbContext _context;

    public EFCreateStructureOfMessageDataProvider(BotDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(IEnumerable<MessageStructure> entity, CancellationToken cancellationToken = default)
    {
        await _context.MessageStructures.AddRangeAsync(entity, cancellationToken);
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid categoryId, CancellationToken ct)
    {
        return await _context.Categories.FirstOrDefaultAsync(x=>x.Id == categoryId, ct);
    }
}