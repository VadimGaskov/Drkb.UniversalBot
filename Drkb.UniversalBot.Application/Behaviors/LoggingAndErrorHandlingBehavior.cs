using System.Reflection;
using Drkb.ResultObjects;
using Drkb.UniversalBot.Application.Interfaces;
using Drkb.UniversalBot.Application.Interfaces.Authorization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Application.Behaviors;

public class LoggingAndErrorHandlingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TResponse : IServerError<TResponse>
{
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<LoggingAndErrorHandlingBehavior<TRequest, TResponse>> _logger;

    public LoggingAndErrorHandlingBehavior(
        ICurrentUserService currentUser,
        ILogger<LoggingAndErrorHandlingBehavior<TRequest, TResponse>> logger)
    {
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                "Ошибка обработки запроса {RequestName} для пользователя {UserId}. Данные запроса: {@Request}",
                typeof(TRequest).Name,
                _currentUser.UserId,
                request);

            return TResponse.ServerError();
        }
    }
}