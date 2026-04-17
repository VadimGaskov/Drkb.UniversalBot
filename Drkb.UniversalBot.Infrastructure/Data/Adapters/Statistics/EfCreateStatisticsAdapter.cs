using Drkb.UniversalBot.Application.UseCase.Command.Statistics.CreateStatistics;
using Drkb.UniversalBot.Domain.Entity;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.Adapters.Statistics;

public class EfCreateStatisticsAdapter: ICreateStatisticsPort
{
    private readonly BotDbContext _context;

    public EfCreateStatisticsAdapter(BotDbContext context)
    {
        _context = context;
    }

    public async Task CreateStatisticsAsync(SenderUser user, string categoryId)
    {
        var statistics = new Domain.Entity.Statistics()
        {
            SenderUser = user,
            Date = DateTime.UtcNow,
        };
        
        var category = await _context.Categories.FirstOrDefaultAsync(x=> x.Id == Guid.Parse(categoryId));
        if (category != null)
            statistics.CategoryId = category.Id;
        
        await _context.Statistics.AddAsync(statistics);
    }

    public async Task CreateStatisticsAsync(SenderUser user)
    {
        var statistics = new Domain.Entity.Statistics()
        {
            SenderUser = user,
            Date = DateTime.UtcNow,
        };
        await _context.Statistics.AddAsync(statistics);
    }

    public async Task<SenderUser> CreateUserAsync(string userId, Messenger userMessenger)
    {
        var user = await _context.SenderUsers.FirstOrDefaultAsync(x=>x.ExternalId == userId);
        if (user != null)
            return user;

        user = new SenderUser()
        {
            Messenger = userMessenger,
            ExternalId = userId,
        };
        await _context.SenderUsers.AddAsync(user);
        return user;
    }
}