namespace PozitronDev.SharedKernel.Contracts;

public interface IAppLogger<T>
{
    public void LogTrace(string? message);
    public void LogTrace<T0>(string? message, T0 arg);
    public void LogTrace<T0, T1>(string? message, T0 arg0, T1 arg1);
    public void LogTrace<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogTrace(string? message, params object?[] args);

    public void LogTrace(Exception? exception, string? message);
    public void LogTrace<T0>(Exception? exception, string? message, T0 arg);
    public void LogTrace<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    public void LogTrace<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogTrace(Exception? exception, string? message, params object?[] args);

    public void LogDebug(string? message);
    public void LogDebug<T0>(string? message, T0 arg);
    public void LogDebug<T0, T1>(string? message, T0 arg0, T1 arg1);
    public void LogDebug<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogDebug(string? message, params object?[] args);

    public void LogDebug(Exception? exception, string? message);
    public void LogDebug<T0>(Exception? exception, string? message, T0 arg);
    public void LogDebug<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    public void LogDebug<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogDebug(Exception? exception, string? message, params object?[] args);

    public void LogInformation(string? message);
    public void LogInformation<T0>(string? message, T0 arg);
    public void LogInformation<T0, T1>(string? message, T0 arg0, T1 arg1);
    public void LogInformation<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogInformation(string? message, params object?[] args);

    public void LogInformation(Exception? exception, string? message);
    public void LogInformation<T0>(Exception? exception, string? message, T0 arg);
    public void LogInformation<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    public void LogInformation<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogInformation(Exception? exception, string? message, params object?[] args);

    public void LogWarning(string? message);
    public void LogWarning<T0>(string? message, T0 arg);
    public void LogWarning<T0, T1>(string? message, T0 arg0, T1 arg1);
    public void LogWarning<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogWarning(string? message, params object?[] args);

    public void LogWarning(Exception? exception, string? message);
    public void LogWarning<T0>(Exception? exception, string? message, T0 arg);
    public void LogWarning<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    public void LogWarning<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogWarning(Exception? exception, string? message, params object?[] args);

    public void LogError(string? message);
    public void LogError<T0>(string? message, T0 arg);
    public void LogError<T0, T1>(string? message, T0 arg0, T1 arg1);
    public void LogError<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogError(string? message, params object?[] args);

    public void LogError(Exception? exception, string? message);
    public void LogError<T0>(Exception? exception, string? message, T0 arg);
    public void LogError<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    public void LogError<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogError(Exception? exception, string? message, params object?[] args);

    public void LogCritical(string? message);
    public void LogCritical<T0>(string? message, T0 arg);
    public void LogCritical<T0, T1>(string? message, T0 arg0, T1 arg1);
    public void LogCritical<T0, T1, T2>(string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogCritical(string? message, params object?[] args);

    public void LogCritical(Exception? exception, string? message);
    public void LogCritical<T0>(Exception? exception, string? message, T0 arg);
    public void LogCritical<T0, T1>(Exception? exception, string? message, T0 arg0, T1 arg1);
    public void LogCritical<T0, T1, T2>(Exception? exception, string? message, T0 arg0, T1 arg1, T2 arg2);
    public void LogCritical(Exception? exception, string? message, params object?[] args);
}
