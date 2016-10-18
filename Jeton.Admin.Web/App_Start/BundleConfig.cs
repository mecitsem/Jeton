using System.Web;
using System.Web.Optimization;

namespace Jeton.Admin.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
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

            //bundles.Add(new ScriptBundle("~/Content/bootstrapper").Include(
            //          "~/Content/vendor/metisMenu/metisMenu.min.js",
            //          "~/Content/vendor/raphael/raphael.min.js",
            //          "~/Content/vendor/morrisjs/morris.min.js",
            //          "~/Content/data/morris-data.js",
            //          "~/Content/dist/js/sb-admin-2.js"
            //    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"
                      ));

            //bundles.Add(new StyleBundle("~/Content/jetoncss").Include(
            //          "~/Content/vendor/metisMenu/metisMenu.min.css",
            //          "~/Content/dist/css/sb-admin-2.css",
            //          "~/Content/vendor/morrisjs/morris.css",
            //          "~/Content/vendor/font-awesome/css/font-awesome.min.css"
            //    ));

        }
    }
}
