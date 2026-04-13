using Drkb.UniversalBot.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Drkb.UniversalBot.Infrastructure.Services;

public class SerilogLoggerService: ILoggerService
{
    private readonly ILogger<SerilogLoggerService> _logger;

    public SerilogLoggerService(ILogger<SerilogLoggerService> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }

    public void LogWarning(string message)
    {
        _logger.LogWarning(message);
    }

    public void LogError(string message, Exception? e)
    {
        _logger.LogError(e, message);
    }

    public void LogError(string message)
    {
        _logger.LogError(message);
    }
}