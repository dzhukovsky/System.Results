using Microsoft.Extensions.Logging;
using System.Results.Contracts;

namespace System.Results.Logging.Extensions;
public static class LoggerExtensions
{
    public static void Log(this ILogger logger, LogLevel logLevel, IError error)
    {
        ArgumentNullException.ThrowIfNull(error);
        logger.Log(logLevel, error.Message);
    }

    public static void Log(this ILogger logger, LogLevel logLevel, Exception? exception, IError error)
    {
        ArgumentNullException.ThrowIfNull(error);
        logger.Log(logLevel, exception, error.Message);
    }

    public static void LogTrace(this ILogger logger, IError error)
        => logger.Log(LogLevel.Trace, error);

    public static void LogTrace(this ILogger logger, Exception? exception, IError error)
        => logger.Log(LogLevel.Trace, exception, error);

    public static void LogDebug(this ILogger logger, IError error)
        => logger.Log(LogLevel.Debug, error);

    public static void LogDebug(this ILogger logger, Exception? exception, IError error)
        => logger.Log(LogLevel.Debug, exception, error);

    public static void LogInformation(this ILogger logger, IError error)
        => logger.Log(LogLevel.Information, error);

    public static void LogInformation(this ILogger logger, Exception? exception, IError error)
        => logger.Log(LogLevel.Information, exception, error);

    public static void LogWarning(this ILogger logger, IError error)
        => logger.Log(LogLevel.Warning, error);

    public static void LogWarning(this ILogger logger, Exception? exception, IError error)
        => logger.Log(LogLevel.Warning, exception, error);

    public static void LogError(this ILogger logger, IError error)
        => logger.Log(LogLevel.Error, error);

    public static void LogError(this ILogger logger, Exception? exception, IError error)
        => logger.Log(LogLevel.Error, exception, error);

    public static void LogCritical(this ILogger logger, IError error)
        => logger.Log(LogLevel.Critical, error);

    public static void LogCritical(this ILogger logger, Exception? exception, IError error)
        => logger.Log(LogLevel.Critical, exception, error);
}
