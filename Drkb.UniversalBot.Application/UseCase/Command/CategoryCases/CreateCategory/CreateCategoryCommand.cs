using Drkb.ResultObjects;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;

public record CreateMessageStructurePayload
{
    public string? Name { get; set; } = null!;
    public string? Value { get; set; } = null!;
    public AppFile? File { get; set; }
    public int Seq { get; set; }
    public TypeField TypeField { get; set; }
} 

public record CreateCategoryCommand: IRequest<Result>
{
    public string NameCategory { get; set; } = null!;
    public Guid? ParentCategoryId { get; set; }
    public List<CreateMessageStructurePayload> Payloads { get; set; } = [];
}