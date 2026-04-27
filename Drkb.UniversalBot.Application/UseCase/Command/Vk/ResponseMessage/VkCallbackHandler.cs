using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Application.Interfaces.Ports;
using MassTransit;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Publisher;

namespace Drkb.UniversalBot.Application.UseCase.Command.VkApi.ResponseMessage;

public class VkCallbackHandler: IRequestHandler<VkCallbackCommand, Result>
{
    private readonly IPublishEndpoint _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public VkCallbackHandler(IPublishEndpoint publisher, IUnitOfWork unitOfWork)
    {
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(VkCallbackCommand request, CancellationToken cancellationToken)
    {
        await _publisher.Publish(request.VkMessageCreated, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}