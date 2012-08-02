using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bieb.Web.Helpers
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            object htmlAttributes = null;

            if (controllerName == currentController 
                || (controllerName == "Book" && currentController == "Story"))
            {
                htmlAttributes = new { @class = "active" };
            }

            return htmlHelper.ActionLink(linkText, actionName, controllerName, null, htmlAttributes);
        }

    }
}