using Drkb.ResultObjects;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.CategoryCases.CreateCategory;

public record CreateCategoryCommand: IRequest<Result>
{
    public string NameCategory { get; init; } = null!;
    public string? Value { get; init; } = null!;
    public Guid? ParentCategoryId { get; init; }
    public Guid UploadContextId { get; set; }
    public List<Guid> FileIds { get; set; }
}