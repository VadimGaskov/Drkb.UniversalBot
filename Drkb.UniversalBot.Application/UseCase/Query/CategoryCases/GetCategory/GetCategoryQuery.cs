using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategory;

public record GetCategoryQuery(Guid Id) : IRequest<Result<GetCategoryDto>>;
