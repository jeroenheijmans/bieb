﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace Bieb.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                        "~/Content/bootstrap/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryplugins").Include(
                        "~/Scripts/select2.js"));

            bundles.Add(new ScriptBundle("~/jsbundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                        "~/Content/bootstrap/css/bootstrap.css",
                        "~/Content/styles/site.css"));

            bundles.Add(new StyleBundle("~/Content/styles/select2").Include(
                        "~/Content/css/select2.css"));
        }
    }
}