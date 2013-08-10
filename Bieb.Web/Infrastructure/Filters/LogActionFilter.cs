using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bieb.Framework.Logging;

namespace Bieb.Web.Infrastructure.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger logger;
        private readonly Level logLevel;

        public LogActionFilter(ILogger log, Level logLevel)
        {
            this.logger = log;
            this.logLevel = logLevel;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var message = string.Format(
                    CultureInfo.InvariantCulture,
                    "Executing action {0}.{1}",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName);
            this.logger.Log(this.logLevel, message);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var message = string.Format(
                    CultureInfo.InvariantCulture,
                    "Executed action {0}.{1}",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName);
            this.logger.Log(this.logLevel, message);
        }
    }
}