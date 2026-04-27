using Drkb.UniversalBot.Application.Interfaces.DataProvider;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.UseCase.Command.Statistics.CreateStatistics;

public class CreateStatisticsEventHandler: INotificationHandler<CreateStatisticsEvent>
{
    private readonly ICreateStatisticsPort _createStatisticsPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateStatisticsEventHandler> _logger;

    public CreateStatisticsEventHandler(ICreateStatisticsPort createStatisticsPort, IUnitOfWork unitOfWork, ILogger<CreateStatisticsEventHandler> logger)
    {
        _createStatisticsPort = createStatisticsPort;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(CreateStatisticsEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Запрошено сохранение статистики обращения. UserId: {UserId}, Messenger: {Messenger}, CategoryId: {CategoryId}",
            notification.UserId,
            notification.Messenger,
            notification.CategortyId);

        var user = await _createStatisticsPort.CreateUserAsync(notification.UserId, notification.Messenger);
        if (notification.CategortyId is not null)
        {
            _logger.LogInformation("Сохраняется статистика по категории. UserId: {UserId}, CategoryId: {CategoryId}",
                notification.UserId,
                notification.CategortyId);
            await _createStatisticsPort.CreateStatisticsAsync(user, notification.CategortyId);
        }
        else
        {
            _logger.LogInformation("Сохраняется статистика без категории. UserId: {UserId}", notification.UserId);
            await _createStatisticsPort.CreateStatisticsAsync(user);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation(
            "Статистика обращения успешно сохранена. UserId: {UserId}, CategoryId: {CategoryId}",
            notification.UserId,
            notification.CategortyId);
    }
}