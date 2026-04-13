using Drkb.ResultObjects;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Publisher;

namespace Drkb.UniversalBot.Application.UseCase.Command.VkApi.ResponseMessage;

public class ResponseMessageHandler: IRequestHandler<ResponseMessageCommand, Result>
{
    private readonly IMessageBrokerPublisher _publisher;

    public ResponseMessageHandler(IMessageBrokerPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task<Result> Handle(ResponseMessageCommand request, CancellationToken cancellationToken)
    {
        await _publisher.PublishAsync(request.Message, cancellationToken);
        return Result.Success();
    }
}