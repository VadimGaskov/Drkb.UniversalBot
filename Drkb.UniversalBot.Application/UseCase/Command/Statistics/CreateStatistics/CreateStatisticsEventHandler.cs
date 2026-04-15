using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Command.Statistics.CreateStatistics;

public class CreateStatisticsEventHandler: INotificationHandler<CreateStatisticsEvent>
{
    private readonly ICreateStatisticsDataProvider _createStatisticsDataProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStatisticsEventHandler(ICreateStatisticsDataProvider createStatisticsDataProvider, IUnitOfWork unitOfWork)
    {
        _createStatisticsDataProvider = createStatisticsDataProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateStatisticsEvent notification, CancellationToken cancellationToken)
    {
        var user = await _createStatisticsDataProvider.CreateUserAsync(notification.UserId, notification.Messenger);
        if (notification.CategortyId is not null)
            await _createStatisticsDataProvider.CreateStatisticsAsync(user, notification.CategortyId);
        else
            await _createStatisticsDataProvider.CreateStatisticsAsync(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}