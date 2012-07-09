﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Supr.Web.Helpers
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            if (controllerName == currentController)
            {
                return htmlHelper.ActionLink(
                    linkText,
                    actionName,
                    controllerName,
                    null,
                    new
                    {
                        @class = "active"
                    });
            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName);
        }

    }
}