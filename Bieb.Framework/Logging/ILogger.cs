using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bieb.Framework.Logging
{
    public interface ILogger
    {
        void Log(Level level, string message);
        void Log(Level level, string message, Exception exception);
        void Log(Level level, Exception exception);
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(string message, Exception exception);
        void LogError(Exception exception);
        void LogFatal(string message);
        void LogFatal(string message, Exception exception);
        void LogFatal(Exception exception);
    }
}
