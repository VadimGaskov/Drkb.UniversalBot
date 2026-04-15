using Drkb.ResultObjects;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Publisher;

namespace Drkb.UniversalBot.Application.UseCase.Command.VkApi.ResponseMessage;

public class VkResponseMessageHandler: IRequestHandler<VkResponseMessageCommand, Result>
{
    private readonly IMessageBrokerPublisher _publisher;

    public VkResponseMessageHandler(IMessageBrokerPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task<Result> Handle(VkResponseMessageCommand request, CancellationToken cancellationToken)
    {
        await _publisher.PublishAsync(request.VkMessage, cancellationToken);
        return Result.Success();
    }
}