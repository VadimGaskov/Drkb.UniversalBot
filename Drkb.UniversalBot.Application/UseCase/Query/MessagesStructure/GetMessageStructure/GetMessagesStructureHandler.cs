using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.MessagesStructure.GetMessageStructure;

public class GetMessagesStructureHandler: IRequestHandler<GetMessagesStructureQuery, Result<GetMessagesStructureDto>>
{
    private readonly IMessagesStructureQuery _messagesStructureQuery;

    public GetMessagesStructureHandler(IMessagesStructureQuery messagesStructureQuery)
    {
        _messagesStructureQuery = messagesStructureQuery;
    }

    public async Task<Result<GetMessagesStructureDto>> Handle(GetMessagesStructureQuery request, CancellationToken cancellationToken)
    {
        var result = await _messagesStructureQuery.ExecuteAsync(request, cancellationToken);
        if (result is null)
            return Result<GetMessagesStructureDto>.NotFound("No messages structure found");
        
        return Result<GetMessagesStructureDto>.Success(result);
    }
}