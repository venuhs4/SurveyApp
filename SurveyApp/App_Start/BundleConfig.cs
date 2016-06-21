using System.Web;
using System.Web.Optimization;

namespace SurveyApp
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

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                      "~/Scripts/angular.js"));

            // Admin UI bundles
            bundles.Add(new StyleBundle("~/Content/adminUI").Include(
                "~/bower_components/bootstrap/dist/css/bootstrap.css",
                "~/bower_components/metisMenu/dist/metisMenu.css",
                "~/bower_components/startbootstrap-sb-admin-2/dist/css/timeline.css",
                "~/bower_components/startbootstrap-sb-admin-2/dist/css/sb-admin-2.css",
                "~/bower_components/morrisjs/morris.css",
                "~/bower_components/font-awesome/css/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/adminUI").Include(
                @"~/bower_components/bootstrap/dist/js/bootstrap.js",
                "~/bower_components/metisMenu/dist/metisMenu.js",
                "~/bower_components/raphael/raphael.js",
                "~/bower_components/morrisjs/morris.js",
                "~/bower_components/startbootstrap-sb-admin-2/dist/js/sb-admin-2.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
