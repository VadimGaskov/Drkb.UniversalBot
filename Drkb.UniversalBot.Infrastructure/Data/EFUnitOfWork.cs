using Drkb.UniversalBot.Application.Interfaces.Ports;

namespace Drkb.UniversalBot.Infrastructure.Data;

public class EFUnitOfWork: IUnitOfWork
{
    private readonly BotDbContext _context;

    public EFUnitOfWork(BotDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}