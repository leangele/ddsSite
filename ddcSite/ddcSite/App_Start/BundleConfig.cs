﻿using System.Web;
using System.Web.Optimization;

namespace ddcSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/Scripts/maphilight").Include(
                       "~/Scripts/jquery.maphilight.min.js"));
            
            bundles.Add(new ScriptBundle("~/Scripts/SitesGeneral").Include(
                       "~/Scripts/SitesGeneral.js"));

            bundles.Add(new ScriptBundle("~/Scripts/datetimepicker").Include(
                       "~/Scripts/jquery.datetimepicker.full.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/moment").Include(
                       "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquerydatatables").Include(
                        "~/Scripts/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/alertify").Include(
                       "~/Scripts/alertify.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/cssDatatable").Include(
                    "~/Content/css/jquery.dataTables.min.css",
                    "~/Content/css/jquery.dataTables.min.css"));

            bundles.Add(new StyleBundle("~/Content/alertify").Include(
                    "~/Content/alertifyjs/alertify.min.css",
                    "~/Content/alertifyjs/themes/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include(
                     "~/Content/jquery.datetimepicker.css"));

            BundleTable.EnableOptimizations = true;

        }
    }
}
