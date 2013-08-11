using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bieb.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController()
        { }

        public ErrorController(HttpResponseBase customResponse, HttpContextBase customContext)
            : base(customResponse, customContext)
        { }

        public ActionResult PageNotFound(string url)
        {
            CurrentResponse.StatusCode = (int)HttpStatusCode.NotFound;
            return View("PageNotFound");
        }

    }
}
