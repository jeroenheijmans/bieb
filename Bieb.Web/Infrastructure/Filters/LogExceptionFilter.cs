using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Framework.Logging;

namespace Bieb.Web.Infrastructure.Filters
{
    public class LogExceptionFilter : IExceptionFilter
    {
        private readonly ILogger logger;
        private readonly Level logLevel;

        public LogExceptionFilter(ILogger log, Level logLevel)
        {
            this.logger = log;
            this.logLevel = logLevel;
        }

        public void OnException(ExceptionContext filterContext)
        {
            this.logger.Log(logLevel, filterContext.Exception);
        }
    }
}