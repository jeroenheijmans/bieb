using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace Bieb.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/MainStyles").Include(
                        "~/Content/styles/site.css"));
        }
    }
}