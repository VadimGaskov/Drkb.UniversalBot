using Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.QueryObjects.Categories;

public class EFGetDataCategoryByIdQuery: IGetDataCategoryByIdQuery
{
    private readonly BotDbContext _context;

    public EFGetDataCategoryByIdQuery(BotDbContext context)
    {
        _context = context;
    }

    public async Task<GetDataCategoryByIdDto?> ExecuteAsync(GetDataCategoryByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await _context.Categories.Where(x => x.Id == query.CategoryId)
            .Select(x => new GetDataCategoryByIdDto
            {
                Categories = x.ChildrenCategories,
                PathFiles = x.StructureOfMessages
                    .Where(sm => sm.TypeField == TypeField.File && sm.StoredFilePath != null)
                    .Select(sm => sm.StoredFilePath).ToList(),
                Text = x.StructureOfMessages
                    .Where(sm => sm.TypeField == TypeField.Text)
                    .Select(sm=>sm.Value)
                    .FirstOrDefault(),
            }).FirstOrDefaultAsync(cancellationToken);
    }
}