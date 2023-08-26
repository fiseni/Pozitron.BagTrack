using Microsoft.Extensions.Logging;
using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.Extensions.Logging;

public class LoggerAdapter<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void Log(LogLevel logLevel, string? message)
    {
        if (_logger.IsEnabled(logLevel))
        {
            _logger.Log(logLevel, message);
        }
    }
    public void Log<T0>(LogLevel logLevel, string? message, T0 arg)
    {
        if (_logger.IsEnabled(logLevel))
        {
            _logger.Log(logLevel, message, arg);
        }
    }
    public void Log<T0, T1>(LogLevel logLevel, string? message, T0 arg0, T1 arg1)
    {
        if (_logger.IsEnabled(logLevel))
        {
            _logger.Log(logLevel, message, arg0, arg1);
        }
    }
    public void Log<T0, T1, T2>(LogLevel logLevel, string? message, T0 arg0, T1 arg1, T2 arg2)
    {
        if (_logger.IsEnabled(logLevel))
        {
            _logger.Log(logLevel, message, arg0, arg1, arg2);
        }
    }
    public void Log(LogLevel logLevel, string? message, params object?[] args)
    {
        if (_logger.IsEnabled(logLevel))
        {
            _logger.Log(logLevel, message, args);
        }
    }

    public void Log(LogLevel logLevel, Exception? exception, string? message)
    {
        if (_logger.IsEnabled(logLevel))
        {
            _logger.Log(logLevel, exception, message);
        }
    }
    public void Log<T0>(LogLevel logLevel, Exception? exception, string? message, T0 arg)
    {
        if (_logger.IsEnabled(logLevel))
        {
            _logger.Log(logLevel, exception, message, arg);
        }
    }
    public void Log<T0, T1>(LogLevel logLevel, Exception? exception, string? message, T0 arg0, T1 arg1)
    {
        if (_logger.IsEnabled(logLevel))
        {
            _logger.Log(logLevel, exception, message, arg0, arg1);
        }
    }
    public void Log<T0, T1, T2>(LogLevel logLevel, Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2)
    {
        if (_logger.IsEnabled(logLevel))
        {
            _logger.Log(logLevel, exception, message, arg0, arg1, arg2);
        }
    }
    public void Log(LogLevel logLevel, Exception? exception, string? message, params object?[] args)
    {
        if (_logger.IsEnabled(logLevel))
        {
            _logger.Log(logLevel, exception, message, args);
        }
    }

    public void LogTrace(string? message) => Log(LogLevel.Trace, message);
    public void LogTrace<T0>(string? message, T0 arg) => Log(LogLevel.Trace, message, arg);
    public void LogTrace<T0, T1>(string? message, T0 arg0, T1 arg1) => Log(LogLevel.Trace, message, arg0, arg1);
    public void LogTrace<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Trace, message, arg0, arg1, arg2);
    public void LogTrace(string? message, params object?[] args) => Log(LogLevel.Trace, message, args);

    public void LogTrace(Exception? exception, string? message) => Log(LogLevel.Trace, exception, message);
    public void LogTrace<T0>(Exception? exception, string? message, T0 arg) => Log(LogLevel.Trace, exception, message, arg);
    public void LogTrace<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1) => Log(LogLevel.Trace, exception, message, arg0, arg1);
    public void LogTrace<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Trace, exception, message, arg0, arg1, arg2);
    public void LogTrace(Exception? exception, string? message, params object?[] args) => Log(LogLevel.Trace, exception, message, args);

    public void LogDebug(string? message) => Log(LogLevel.Debug, message);
    public void LogDebug<T0>(string? message, T0 arg) => Log(LogLevel.Debug, message, arg);
    public void LogDebug<T0, T1>(string? message, T0 arg0, T1 arg1) => Log(LogLevel.Debug, message, arg0, arg1);
    public void LogDebug<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Debug, message, arg0, arg1, arg2);
    public void LogDebug(string? message, params object?[] args) => Log(LogLevel.Debug, message, args);

    public void LogDebug(Exception? exception, string? message) => Log(LogLevel.Debug, exception, message);
    public void LogDebug<T0>(Exception? exception, string? message, T0 arg) => Log(LogLevel.Debug, exception, message, arg);
    public void LogDebug<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1) => Log(LogLevel.Debug, exception, message, arg0, arg1);
    public void LogDebug<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Debug, exception, message, arg0, arg1, arg2);
    public void LogDebug(Exception? exception, string? message, params object?[] args) => Log(LogLevel.Debug, exception, message, args);

    public void LogInformation(string? message) => Log(LogLevel.Information, message);
    public void LogInformation<T0>(string? message, T0 arg) => Log(LogLevel.Information, message, arg);
    public void LogInformation<T0, T1>(string? message, T0 arg0, T1 arg1) => Log(LogLevel.Information, message, arg0, arg1);
    public void LogInformation<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Information, message, arg0, arg1, arg2);
    public void LogInformation(string? message, params object?[] args) => Log(LogLevel.Information, message, args);

    public void LogInformation(Exception? exception, string? message) => Log(LogLevel.Information, exception, message);
    public void LogInformation<T0>(Exception? exception, string? message, T0 arg) => Log(LogLevel.Information, exception, message, arg);
    public void LogInformation<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1) => Log(LogLevel.Information, exception, message, arg0, arg1);
    public void LogInformation<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Information, exception, message, arg0, arg1, arg2);
    public void LogInformation(Exception? exception, string? message, params object?[] args) => Log(LogLevel.Information, exception, message, args);

    public void LogWarning(string? message) => Log(LogLevel.Warning, message);
    public void LogWarning<T0>(string? message, T0 arg) => Log(LogLevel.Warning, message, arg);
    public void LogWarning<T0, T1>(string? message, T0 arg0, T1 arg1) => Log(LogLevel.Warning, message, arg0, arg1);
    public void LogWarning<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Warning, message, arg0, arg1, arg2);
    public void LogWarning(string? message, params object?[] args) => Log(LogLevel.Warning, message, args);

    public void LogWarning(Exception? exception, string? message) => Log(LogLevel.Warning, exception, message);
    public void LogWarning<T0>(Exception? exception, string? message, T0 arg) => Log(LogLevel.Warning, exception, message, arg);
    public void LogWarning<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1) => Log(LogLevel.Warning, exception, message, arg0, arg1);
    public void LogWarning<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Warning, exception, message, arg0, arg1, arg2);
    public void LogWarning(Exception? exception, string? message, params object?[] args) => Log(LogLevel.Warning, exception, message, args);

    public void LogError(string? message) => Log(LogLevel.Error, message);
    public void LogError<T0>(string? message, T0 arg) => Log(LogLevel.Error, message, arg);
    public void LogError<T0, T1>(string? message, T0 arg0, T1 arg1) => Log(LogLevel.Error, message, arg0, arg1);
    public void LogError<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Error, message, arg0, arg1, arg2);
    public void LogError(string? message, params object?[] args) => Log(LogLevel.Error, message, args);

    public void LogError(Exception? exception, string? message) => Log(LogLevel.Error, exception, message);
    public void LogError<T0>(Exception? exception, string? message, T0 arg) => Log(LogLevel.Error, exception, message, arg);
    public void LogError<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1) => Log(LogLevel.Error, exception, message, arg0, arg1);
    public void LogError<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Error, exception, message, arg0, arg1, arg2);
    public void LogError(Exception? exception, string? message, params object?[] args) => Log(LogLevel.Error, exception, message, args);

    public void LogCritical(string? message) => Log(LogLevel.Critical, message);
    public void LogCritical<T0>(string? message, T0 arg) => Log(LogLevel.Critical, message, arg);
    public void LogCritical<T0, T1>(string? message, T0 arg0, T1 arg1) => Log(LogLevel.Critical, message, arg0, arg1);
    public void LogCritical<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Critical, message, arg0, arg1, arg2);
    public void LogCritical(string? message, params object?[] args) => Log(LogLevel.Critical, message, args);

    public void LogCritical(Exception? exception, string? message) => Log(LogLevel.Critical, exception, message);
    public void LogCritical<T0>(Exception? exception, string? message, T0 arg) => Log(LogLevel.Critical, exception, message, arg);
    public void LogCritical<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1) => Log(LogLevel.Critical, exception, message, arg0, arg1);
    public void LogCritical<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2) => Log(LogLevel.Critical, exception, message, arg0, arg1, arg2);
    public void LogCritical(Exception? exception, string? message, params object?[] args) => Log(LogLevel.Critical, exception, message, args);
}
