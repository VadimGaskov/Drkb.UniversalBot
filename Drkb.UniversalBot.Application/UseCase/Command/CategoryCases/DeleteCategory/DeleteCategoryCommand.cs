using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.DeleteCategory;

public record DeleteCategoryCommand(Guid CategoryId): IRequest<Result>;