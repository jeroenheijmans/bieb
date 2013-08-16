using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bieb.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly HttpResponseBase customResponse;


        protected HttpResponseBase CurrentResponse
        {
            get { return customResponse ?? this.Response; }
        }


        public BaseController()
        { }


        public BaseController(HttpResponseBase customResponse)
        {
            this.customResponse = customResponse;
        }


        public ActionResult PageNotFound()
        {
            CurrentResponse.StatusCode = (int)HttpStatusCode.NotFound;
            return View("PageNotFound");
        }
    }
}