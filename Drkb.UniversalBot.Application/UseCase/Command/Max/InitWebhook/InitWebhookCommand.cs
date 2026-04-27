using Drkb.ResultObjects;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.Max.InitWebhook;

public record InitWebhookCommand(): IRequest<Result>;