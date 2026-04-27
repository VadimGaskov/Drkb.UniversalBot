using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Events;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.Max.ResponseMessage;

public record MaxCallbackCommand(MaxMessageCreatedEvent MaxMessageCreated): IRequest<Result>;