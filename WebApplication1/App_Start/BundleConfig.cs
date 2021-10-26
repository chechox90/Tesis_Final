using System.Web;
using System.Web.Optimization;

namespace WebApplication1
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            RegisterJquery(bundles);
            RegisterPlugins(bundles);
            RegisterLogin(bundles);
            RegisterGeneralFunctions(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterJquery(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Plugins/jquery").Include(
                        "~/Plugins/jquery/jquery-{version}.js"));
        }

        private static void RegisterLogin(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Account/Login/js").Include(
                "~/Scripts/Account/login.js"));
        }

        private static void RegisterGeneralFunctions(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/GeneralFunctions/js").Include(
                "~/Scripts/general-functions.js"));
        }

        private static void RegisterPlugins(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Plugins/JqueryUi/css").Include("~/Plugins/JqueryUi/css/jquery-ui.css"));
            bundles.Add(new StyleBundle("~/Plugins/SweetAlerts2/css").Include("~/Plugins/SweetAlerts2/css/sweetalert2.min.css"));
            bundles.Add(new StyleBundle("~/Plugins/FontAwesome/css").Include("~/Plugins/FontAwesome/css/fontawesome.css"));
            bundles.Add(new StyleBundle("~/Plugins/Normalize/css").Include("~/Plugins/Normalize/normalize.css"));
            bundles.Add(new StyleBundle("~/Plugins/Bootstrap/css").Include(
                "~/Plugins/Bootstrap/css/bootstrap.min.css",
                "~/Plugins/Bootstrap/css/bootstrap-select.min.css"
                ));
            bundles.Add(new StyleBundle("~/Plugins/DataTables/css").Include(
                "~/Plugins/DataTables/css/dataTables.bootstrap4.min.css",
                "~/Plugins/DataTables/css/buttons.bootstrap4.min.css"
                ));
            bundles.Add(new StyleBundle("~/Content/WebApp/Intranet/css").Include(
                "~/Content/WebApp/intranet.css",
                "~/Content/WebApp/intranet-responsive.css"
                ));

            bundles.Add(new StyleBundle("~/Plugins/JqueryTimePicker/css").Include("~/Plugins/jquery-timepicker/css/jquery.timepicker.min.css"));

            bundles.Add(new StyleBundle("~/Plugins/DateRangePicker/css").Include("~/Plugins/DateRangePicker/css/daterangepicker.css"));

            bundles.Add(new StyleBundle("~/Plugins/Plyr/css").Include("~/Plugins/Plyr/css/plyr.css"));

            bundles.Add(new StyleBundle("~/Plugins/TailSelect/css").Include("~/Plugins/TailSelect/css/tail.select-default.min.css"));

            bundles.Add(new ScriptBundle("~/Plugins/moment").Include("~/Plugins/moment/moment.js"));

            bundles.Add(new ScriptBundle("~/Plugins/JqueryTimePicker/js").Include("~/Plugins/jquery-timepicker/js/jquery.timepicker.min.js"));

            bundles.Add(new ScriptBundle("~/Plugins/SweetAlerts2/js").Include(
                "~/Plugins/SweetAlerts2/js/sweetalert2.min.js",
                "~/Plugins/SweetAlerts2/js/promise.min.js",
                "~/Plugins/SweetAlerts2/js/sweetalerts2control.js"
                ));
            bundles.Add(new ScriptBundle("~/Plugins/ScrollReveal/js").Include(
                "~/Plugins/ScrollReveal/scrollreveal.js",
                "~/Plugins/ScrollReveal/scrollrevealcontrol.js"
                ));
            bundles.Add(new ScriptBundle("~/Plugins/JqueryUi/js").Include("~/Plugins/JqueryUi/js/jquery-ui.js"));
            bundles.Add(new ScriptBundle("~/Plugins/Bootstrap/js").Include(
                "~/Plugins/Bootstrap/js/popper.min.js",
                "~/Plugins/Bootstrap/js/bootstrap.min.js",
                "~/Plugins/Bootstrap/js/bootstrap-select.min.js"
                ));
            bundles.Add(new ScriptBundle("~/Plugins/DataTables/js").Include(
                "~/Plugins/DataTables/js/jquery.dataTables.min.js",
                "~/Plugins/DataTables/js/dataTables.bootstrap4.min.js",
                "~/Plugins/DataTables/js/dataTables.responsive.min.js",
                "~/Plugins/DataTables/js/responsive.bootstrap4.min.js",
                "~/Plugins/DataTables/js/dataTables.buttons.min.js",
                "~/Plugins/DataTables/js/buttons.bootstrap4.min.js",
                "~/Plugins/DataTables/js/jszip.min.js",
                "~/Plugins/DataTables/js/pdfmake.min.js",
                "~/Plugins/DataTables/js/vfs_fonts.js",
                "~/Plugins/DataTables/js/buttons.html5.min.js",
                "~/Plugins/DataTables/js/buttons.print.min.js",
                "~/Plugins/DataTables/js/buttons.colVis.min.js"
                ));
            //new
            bundles.Add(new ScriptBundle("~/Plugins/Highcharts").Include(
                "~/Plugins/Highcharts/highcharts.js",
                "~/Plugins/Highcharts/highcharts-more.js",
                "~/Plugins/Highcharts/highcharts-3d.js",
                "~/Plugins/Highcharts/exporting.js",
                "~/Plugins/Highcharts/export-data.js",
                "~/Plugins/Highcharts/accessibility.js",
                "~/Plugins/Highcharts/solid-gauge.js"
                ));
            //new
            bundles.Add(new ScriptBundle("~/Plugins").Include("~/Plugins/main-js.js"));
            bundles.Add(new ScriptBundle("~/Plugins").Include("~/Plugins/moment.js"));

            bundles.Add(new ScriptBundle("~/Plugins/DateRangePicker/js").Include(
                "~/Plugins/DateRangePicker/js/daterangepicker.js"
                ,
                "~/Plugins/DateRangePicker/js/daterangepickerControl.js"
                ));

            bundles.Add(new ScriptBundle("~/Plugins/Plyr/js").Include("~/Plugins/Plyr/js/plyr.js"));

            bundles.Add(new ScriptBundle("~/Plugins/TailSelect/js").Include(
                "~/Plugins/TailSelect/js/tail.select.min.js",
                "~/Plugins/TailSelect/js/tail.select-es.js",
                "~/Plugins/TailSelect/js/tailSelectControl.js"
                ));

            // plugins | jquery-validate
            bundles.Add(new ScriptBundle("~/Plugins/jquery-validate").Include(
                                         "~/Plugins/jquery-validate/jquery.validate*",
                                         "~/Plugins/jquery-validate/jquery.validate.unobtrusive*"));
            bundles.Add(new ScriptBundle("~/Plugins/ajax").Include("~/Plugins/jquery-unobtrusive-ajax/jquery.unobtrusive-ajax.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Intranet/js").Include("~/Scripts/intranet.js"));

        }
    }
}
