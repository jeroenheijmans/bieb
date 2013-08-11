using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bieb.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly HttpResponseBase customResponse;
        private readonly HttpContextBase customContext;

        protected HttpResponseBase CurrentResponse
        {
            get { return customResponse ?? this.Response; }
        }

        protected HttpContextBase CurrentContext
        {
            get { return customContext ?? this.HttpContext; }
        }

        public BaseController()
        { }

        public BaseController(HttpResponseBase customResponse, HttpContextBase customContext)
        {
            this.customResponse = customResponse;
            this.customContext = customContext;
        }


        protected override void HandleUnknownAction(string actionName)
        {
            // If controller is ErrorController dont 'nest' exceptions
            if (this.GetType() != typeof(ErrorController))
                InvokeHttp404();
        }

        public ActionResult InvokeHttp404()
        {
            if (CurrentContext == null)
            {
                throw new Exception("Current HttpContext was null. This method depends on a context.");
            }

            // TODO: get the controller from our container, so that this method can test that the ErrorController's method is called with correct route data
            var errorController = new ErrorController(CurrentResponse, CurrentContext);

            var errorRoute = new RouteData();
            errorRoute.Values.Add("controller", "Error");
            errorRoute.Values.Add("action", "PageNotFound");

            if (CurrentContext.Request != null && CurrentContext.Request.Url != null)
            {
                errorRoute.Values.Add("url", CurrentContext.Request.Url.OriginalString);
            }

            errorController.Execute(new RequestContext(CurrentContext, errorRoute));

            return new EmptyResult();
        }
    }
}