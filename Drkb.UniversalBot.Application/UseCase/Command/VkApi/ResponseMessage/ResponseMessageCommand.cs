using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Application.Interfaces.VkIntegration.CallbackDtos;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.VkApi.ResponseMessage;

public record ResponseMessageCommand(MessageEvent Message) : IRequest<Result>;
