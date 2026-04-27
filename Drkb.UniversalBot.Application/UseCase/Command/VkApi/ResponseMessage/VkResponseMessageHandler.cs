using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Events;
using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using MassTransit;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Publisher;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Command.VkApi.ResponseMessage;

public class VkResponseMessageHandler: IRequestHandler<VkResponseMessageCommand, Result>
{
    private readonly IPublishEndpoint _publisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<VkResponseMessageHandler> _logger;

    public VkResponseMessageHandler(IPublishEndpoint publisher, IUnitOfWork unitOfWork, ILogger<VkResponseMessageHandler> logger)
    {
        _publisher = publisher;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(VkResponseMessageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрошена публикация события исходящего сообщения VK");
        await _publisher.Publish(request.VkMessageCreated, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Событие исходящего сообщения VK успешно опубликовано");
        return Result.Success();
    }
}