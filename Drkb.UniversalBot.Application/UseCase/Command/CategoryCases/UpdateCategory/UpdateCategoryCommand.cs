using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;

public record UpdateCategoryCommand(Guid Id, string Name, Guid? ParentCategoryId): IRequest<Result>;
