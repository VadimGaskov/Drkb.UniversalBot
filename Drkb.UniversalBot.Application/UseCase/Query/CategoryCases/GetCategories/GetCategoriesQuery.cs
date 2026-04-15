using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategories;

public record GetCategoriesQuery(): IRequest<Result<List<GetCategoriesDto>>>;