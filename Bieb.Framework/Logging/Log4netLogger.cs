using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using log4net;

namespace Bieb.Framework.Logging
{
    /// <summary>
    /// ILogger implementation using the log4net package. Configuration of log4net is placed in the AssemblyInfo file.
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        private readonly ILog logger;

        public Log4NetLogger()
        {
            logger = LogManager.GetLogger(this.GetType());
        }

        public void Log(Level level, string message)
        {
            Log(level, message, null);
        }

        public void Log(Level level, string message, Exception exception)
        {
            switch (level)
            {
                case Level.Debug:
                    LogDebug(message);
                    break;
                case Level.Information:
                    LogInformation(message);
                    break;
                case Level.Warning:
                    LogWarning(message);
                    break;
                case Level.Error:
                    LogError(message, exception);
                    break;
                case Level.Fatal:
                    LogFatal(message, exception);
                    break;
                default:
                    LogError("Logging called with unknown level '" + level.ToString() + "'. Message will be logged with substitute level 'information'.");
                    LogInformation(message);
                    break;
            }
        }

        public void Log(Level level, Exception exception)
        {
            Debug.Assert(exception != null, "Argument 'exception' should not be null: logging nothing is quite pointless.");
            Log(level, "An error occurred.", exception);
        }
        
        public void LogInformation(string message)
        {
            logger.Info(message);
        }

        public void LogWarning(string message)
        {
            logger.Warn(message);
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            LogError(message, null);
        }

        public void LogError(string message, Exception exception)
        {
            logger.Error(message, exception);
        }

        public void LogError(Exception exception)
        {
            LogError("An error occurred.", exception);
        }

        public void LogFatal(string message)
        {
            logger.Fatal(message);
        }

        public void LogFatal(string message, Exception exception)
        {
            logger.Fatal(message, exception);
        }

        public void LogFatal(Exception exception)
        {
            LogFatal("A fatal error occurred.", exception);
        }
    }
}
