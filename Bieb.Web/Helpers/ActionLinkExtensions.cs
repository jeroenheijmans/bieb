using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bieb.Web.Helpers
{
    public static class ActionLinkExtensions
    {
        public static string GetCssClass(this HtmlHelper htmlHelper, string controllerName)
        {
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            if (controllerName == currentController
                // TODO: Refactor the following two "Exceptions" to a more solid solution.
                || (controllerName == "Books" && currentController == "Story")
                || (controllerName == "Books" && currentController == "Stories"))
            {
                return "active";
            }
            return "";
        }
    }
}