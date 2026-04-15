using Drkb.UniversalBot.Application.UseCase.Query.MessagesStructure.GetMessageStructure;
using Microsoft.EntityFrameworkCore;

namespace Drkb.UniversalBot.Infrastructure.Data.QueryObjects.MessagesStructures;

public class EFMessagesStructureQuery: IMessagesStructureQuery
{
    private BotDbContext _context;

    public EFMessagesStructureQuery(BotDbContext context)
    {
        _context = context;
    }

    public async Task<GetMessagesStructureDto?> ExecuteAsync(GetMessagesStructureQuery query, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .Where(x => x.Id == query.CategoryId)
            .Select(x => new GetMessagesStructureDto
            {
                CategoryId = x.Id,
                CategoryTitle = x.Title,
                MessagesStructureDtos = x.StructureOfMessages.Select(sm => new MessagesStructureDto
                {
                    Seq = sm.Seq,
                    Name = sm.Title,
                    Value = sm.Value,
                    TypeField = sm.TypeField,
                    OriginalFileName = sm.OriginalFileName
                }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);
    }
}