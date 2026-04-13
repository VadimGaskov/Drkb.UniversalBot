using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.MessagesStructure.GetMessageStructure;

public record GetMessagesStructureQuery(Guid CategoryId): IRequest<Result<GetMessagesStructureDto>>;