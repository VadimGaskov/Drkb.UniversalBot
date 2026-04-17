using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.Statistics.CreateStatistics;

public class CreateStatisticsEventHandler: INotificationHandler<CreateStatisticsEvent>
{
    private readonly ICreateStatisticsPort _createStatisticsPort;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStatisticsEventHandler(ICreateStatisticsPort createStatisticsPort, IUnitOfWork unitOfWork)
    {
        _createStatisticsPort = createStatisticsPort;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateStatisticsEvent notification, CancellationToken cancellationToken)
    {
        var user = await _createStatisticsPort.CreateUserAsync(notification.UserId, notification.Messenger);
        if (notification.CategortyId is not null)
            await _createStatisticsPort.CreateStatisticsAsync(user, notification.CategortyId);
        else
            await _createStatisticsPort.CreateStatisticsAsync(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}