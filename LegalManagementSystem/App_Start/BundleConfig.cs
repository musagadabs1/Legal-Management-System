using System.Web;
using System.Web.Optimization;

namespace LegalManagementSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/Select2/js").Include(
                "~/Scripts/select2.js"
                ));
            bundles.Add(new ScriptBundle("~/admin-lte/js").Include(
             "~/admin-lte/js/app.js",
             "~/admin-lte/plugins/fastclick/fastclick.js",
             "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"
             ));

            bundles.Add(new ScriptBundle("~/nalika-tempalte/js").Include(
                "~/Nalika/nalika/js/wow.min.js",
                "~/Nalika/nalika/js/jquery-price-slider.js",
                "~/Nalika/nalika/js/jquery.meanmenu.js",
                "~/Nalika/nalika/js/owl.carousel.min.js",
                "~/Nalika/nalika/js/jquery.sticky.js",
                "~/Nalika/nalika/js/jquery.scrollUp.min.js",
                "~/Nalika/nalika/js/scrollbar/jquery.mCustomScrollbar.concat.min.js",
                "~/Nalika/nalika/js/scrollbar/mCustomScrollbar-active.js",
                "~/Nalika/nalika/js/metisMenu/metisMenu.min.js",
                "~/Nalika/nalika/js/metisMenu/metisMenu-active.js",
                "~/Nalika/nalika/js/sparkline/jquery.sparkline.min.js",
                "~/Nalika/nalika/js/sparkline/jquery.charts-sparkline.js"
                ));
            bundles.Add(new ScriptBundle("~/Calendar/js").Include(
                "~/Nalika/nalika/js/calendar/moment.min.js",
                "~/Nalika/nalika/js/calendar/fullcalendar.min.js",
                "~/Nalika/nalika/js/calendar/fullcalendar-active.js"
             ));
            bundles.Add(new ScriptBundle("~/float/js").Include(
                "~/Nalika/nalika/js/flot/jquery.flot.js",
                "~/Nalika/nalika/js/flot/jquery.flot.resize.js",
                "~/Nalika/nalika/js/flot/curvedLines.js",
                "~/Nalika/nalika/js/flot/flot-active.js"
             ));
            bundles.Add(new ScriptBundle("~/pluginsAndMain/js").Include(
                "~/Nalika/nalika/js/plugins.js",
                "~/Nalika/nalika/js/main.js"
             ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css",
            //          "~/admin-lte/css/AdminLTE.css",
            //          "~/admin-lte/css/skins/skin-blue.css",
            //          "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"
            //          ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"
                       //"~/Content/styles.css"
                      ));
            bundles.Add(new StyleBundle("~/owlncarousel/css").Include(
                      "~/Nalika/nalika/css/owl.carousel.css",
                      "~/Nalika/nalika/css/owl.theme.css",
                      "~/Nalika/nalika/css/owl.transitions.css"
                      ));
            //bundles.Add(new StyleBundle("~/owlncarousel/css").Include(
            //          "~/Nalika/nalika/css/owl.carousel.css",
            //          "~/Nalika/nalika/css/owl.theme.css",
            //          "~/Nalika/nalika/css/owl.transitions.css"
            //          ));
        }
    }
}
