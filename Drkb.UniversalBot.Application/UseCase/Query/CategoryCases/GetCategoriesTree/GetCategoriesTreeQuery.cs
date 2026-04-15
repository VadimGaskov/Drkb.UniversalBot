using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.CategoryCases.GetCategoriesTree;

public record GetCategoriesTreeQuery(): IRequest<Result<List<CategoriesTreeDto>>>;