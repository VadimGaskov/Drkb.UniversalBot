using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.CreateCategory;      

public record CreateCategoryCommand(string Title, Guid? ParentCategoryId): IRequest<Result>;