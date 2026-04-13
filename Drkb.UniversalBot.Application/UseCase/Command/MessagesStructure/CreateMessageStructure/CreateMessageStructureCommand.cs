using Drkb.ResultObjects;
using Drkb.UniversalBot.Domain.Entity.ValueObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.MessagesStructure.CreateMessageStructure;

public record CreateMessageStructurePayload
{
    public string? Title { get; set; } = null!;
    public string? Value { get; set; } = null!;
    public AppFile? File { get; set; }
    public int Seq { get; set; }
    public TypeField TypeField { get; set; }
} 

public record CreateMessageStructureCommand: IRequest<Result>
{
    public Guid CategoryId { get; set; }
    public List<CreateMessageStructurePayload> Payloads { get; set; } = [];
}