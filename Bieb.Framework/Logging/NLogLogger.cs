using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Bieb.Framework.Logging
{
    // TODO: Hmm I'd prefer not to make this public, but that would require moving the NInject module to Bieb.Framework...
    public class NLogLogger : ILogger
    {
        private Logger logger;

        NLogLogger()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void LogInformation(string message)
        {
            logger.Info(message);
        }

        public void LogWarning(string message)
        {
            logger.Warn(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }
    }
}
