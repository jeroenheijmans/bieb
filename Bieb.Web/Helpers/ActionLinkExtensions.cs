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
                // TODO: Refactor the following two "Exceptions" to a more solid solution.
                || (controllerName == "Books" && currentController == "Story")
                || (controllerName == "Books" && currentController == "Stories"))
            {
                htmlAttributes = new { @class = "active" };
            }

            return htmlHelper.ActionLink(linkText, actionName, controllerName, null, htmlAttributes);
        }

    }
}