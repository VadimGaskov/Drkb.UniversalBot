using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.UpdateCategory;

public record UpdateCategoryCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string NameCategory { get; init; } = null!;
    public string? Value { get; init; } = null!;
    public Guid? ParentCategoryId { get; init; }
    public Guid UploadContextId { get; set; }
    public List<Guid> FileIds { get; set; }
}
