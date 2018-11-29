using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelotonData
{
    public interface ILogger
    {
        void Log(LogEntry entry);
    }

    public enum LoggingEventType { Debug, Information, Warning, Error, Fatal };

    // Immutable DTO that contains the log information.
    public class LogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;
        public readonly Exception Exception;

        public LogEntry(LoggingEventType severity, string message, Exception exception = null)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (message == string.Empty) throw new ArgumentException("empty", "message");

            this.Severity = severity;
            this.Message = message;
            this.Exception = exception;
        }
    }

    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Information, message));
        }

        public static void Log(this ILogger logger, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, exception.Message, exception));
        }

        public static void LogDebug(this ILogger logger, string message, Exception exception = null)
        {
            logger.Log(new LogEntry(LoggingEventType.Debug, message, exception));
        }

        public static void LogWarning(this ILogger logger, string message, Exception exception = null)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, message, exception));
        }

        public static void LogInformational(this ILogger logger, string message, Exception exception = null)
        {
            logger.Log(new LogEntry(LoggingEventType.Information, message, exception));
        }

        public static void LogError(this ILogger logger, string message, Exception exception = null)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, message, exception));
        }
        // More methods here.
    }
}
