namespace Budgetify.Services.Infrastructure.Logger;

using System;

using Budgetify.Contracts.Settings;

using Microsoft.Extensions.Logging;

public class Logger<TCategoryName> : Contracts.Infrastructure.Logger.ILogger<TCategoryName>
{
    private readonly ILogger<TCategoryName> _logger;
    private readonly ILoggerSettings _loggerSettings;

    public Logger(ILogger<TCategoryName> logger, ILoggerSettings loggerSettings)
    {
        _logger = logger;
        _loggerSettings = loggerSettings;
    }

    public void LogError(string message)
    {
        _logger.LogError(message);
    }

    public void LogException(Exception exception, string message)
    {
        _logger.LogError(exception, message);
    }

    public void LogException(Exception exception)
    {
        _logger.LogError(exception, exception.Message);
    }

    public void LogInformation(string message, object data)
    {
        _logger.LogInformation(_loggerSettings.DataMessageTemplate, message, data);
    }
}
