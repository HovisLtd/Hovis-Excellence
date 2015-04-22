using System.Web.Optimization;

namespace Hovis.Excellence.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/bundles/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/vendor/bootstrap-select/bootstrap-select.css",
                "~/Content/vendor/dropzone/dropzone.css",
                "~/Content/vendor/slider/slider.css",
                "~/Content/vendor/bootstrap-datepicker/datepicker.css",
                "~/Content/vendor/timepicker/jquery.timepicker.css",
                "~/Content/vendor/offline/theme.css",
                "~/Content/vendor/pace/theme.css",
                "~/Content/css/animate.css",
                "~/Content/css/skins/palette.1.css",
                "~/Content/css/fonts/style.1.css",
                "~/Content/css/main.css",
                "~/Content/css/custom.css"
                ));

            bundles.Add(new StyleBundle("~/Content/bundles/fonts").Include("~/Content/css/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/content/bundles/scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*"
                        ));

            //bundles.Add(new ScriptBundle("~/content/bundles/scripts/jquery").Include(
            //"~/Scripts/jquery-{version}.js"
            //));

            bundles.Add(new ScriptBundle("~/content/bundles/scripts/modernizr").Include(
                        "~/Content/vendor/modernizr-*"));

            bundles.Add(new ScriptBundle("~/content/bundles/scripts/bootstrap-scripts").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Content/vendor/bootstrap-select/bootstrap-select.js",
                      "~/Content/vendor/bootstrap-datepicker/bootstrap-datepicker.js",
                      "~/Scripts/respond.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}