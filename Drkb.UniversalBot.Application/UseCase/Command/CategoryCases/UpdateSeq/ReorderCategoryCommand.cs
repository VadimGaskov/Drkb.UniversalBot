using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateSeq;

public record ReorderCategory
{
    public Guid Id { get; set; }
    public int Order { get; set; }
}

public class ReorderCategoryCommand: IRequest<Result>
{
    public List<ReorderCategory> ReorderCategories { get; set; } = [];
}