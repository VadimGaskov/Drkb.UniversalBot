using Drkb.UniversalBot.Application.UseCase.Query.Statistics.GetDashboard;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.QueryObjects.Statistics;

public class EFDashboardQuery: IDashboardQuery
{
    private readonly BotDbContext _context;

    public EFDashboardQuery(BotDbContext context)
    {
        _context = context;
    }

    public async Task<GetDashboardDto> ExecuteAsync(CancellationToken cancellationToken)
    {
        var countUsers = await _context.SenderUsers.CountAsync(cancellationToken);
        var countMessages = await _context.Statistics.CountAsync(cancellationToken);
        var countCategories = await _context.Categories.CountAsync(cancellationToken);
        var activeUsersToday = await _context.SenderUsers
            .Where(x=>x.Statistics.Any(s=>s.Date.Date == DateTime.UtcNow.Date))
            .CountAsync(cancellationToken);

        var activeWeek = await _context.Statistics
            .Where(x => x.Date.Date <= DateTime.UtcNow.Date && x.Date.Date >= DateTime.UtcNow.Date.AddDays(-7))
            .Select(x => new
            {
                Date = x.Date.Date,
                Users = x.SenderUser,
                Message = x
            }).GroupBy(x => x.Date)
            .Select(x => new ActiveWeek()
            {
                Date = x.Key.ToShortDateString(),
                CountMessages = x.Select(g => g.Message).Distinct().Count(),
                CountUsers = x.Select(g => g.Users).Distinct().Count(),
            }).ToListAsync(cancellationToken);
        
        var popularCategory = await _context.Categories
            .Select(x=> new PopularCategory()
            {
                Name = x.Title,
                Count = x.Statistics.Count
            }).OrderByDescending(x=>x.Count).Take(3).ToListAsync(cancellationToken);

        return new GetDashboardDto()
        {
            CountUsers = countUsers,
            CountMessages = countMessages,
            ActiveWeeks = activeWeek,
            CountCategories = countCategories,
            PopularCategories = popularCategory,
            ActiveUsersToday = activeUsersToday,
        };
    }
}