using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetDataCategoryById;

public record GetDataCategoryByIdQuery(Guid CategoryId): IRequest<GetRequestDataDto?>;