using System.Reflection;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.Authorization;
using MediatR;

namespace Drkb.UniversalBot.Application.Behaviors;

public class LoggingAndErrorHandlingBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILoggerService _logger;
    private readonly ICurrentUserService _currentUser;

    public LoggingAndErrorHandlingBehavior(ILoggerService logger, ICurrentUserService currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Начало обработки {typeof(TRequest).Name} пользователем {_currentUser.UserId}");
        try
        {
            var response = await next();
            _logger.LogInformation($"Успешно обработано {typeof(TRequest).Name} пользователем {_currentUser.UserId}");
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError($"Ошибка при обработке {typeof(TRequest).Name} пользователем {_currentUser.UserId}", e);
            var method = typeof(TResponse).GetMethod("ServerError", BindingFlags.Public | BindingFlags.Static);
            return (TResponse)method.Invoke(null, null);
        }
    }
}