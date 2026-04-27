using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces.Ports;
using MassTransit;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.Max.ResponseMessage;

public class MaxCallbackHandler: IRequestHandler<MaxCallbackCommand, Result>
{
    private readonly IPublishEndpoint _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public MaxCallbackHandler(IPublishEndpoint publisher, IUnitOfWork unitOfWork)
    {
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(MaxCallbackCommand request, CancellationToken cancellationToken)
    {
        await _publisher.Publish(request.MaxMessageCreated, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}