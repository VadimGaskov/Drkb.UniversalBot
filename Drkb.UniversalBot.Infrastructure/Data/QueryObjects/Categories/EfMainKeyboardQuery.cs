using System.Text.Json;
using System.Text.Json.Serialization;
using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;
using Drkb.UniversalBot.Domain.Entity;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using Drkb.UniversalBot.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.QueryObjects.Categories;

public class EfMainKeyboardQuery: IMainKeyboardQuery
{
    private readonly BotDbContext _context;

    public EfMainKeyboardQuery(BotDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> ExecuteAsync(GetMainKeyboardQuery query,
        CancellationToken cancellationToken = default)
    {
        var categories =
            await _context.Categories.Where(x => x.ParentCategoryId == null && x.CategoryStatus == CategoryStatus.Active).ToListAsync(cancellationToken);

        return categories;
    }
}