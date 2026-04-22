using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using MassTransit;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Publisher;

namespace Drkb.UniversalBot.Application.UseCase.Command.VkApi.ResponseMessage;

public class VkResponseMessageHandler: IRequestHandler<VkResponseMessageCommand, Result>
{
    private readonly IPublishEndpoint _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public VkResponseMessageHandler(IPublishEndpoint publisher, IUnitOfWork unitOfWork)
    {
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(VkResponseMessageCommand request, CancellationToken cancellationToken)
    {
        await _publisher.Publish(request.VkMessageCreated, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}