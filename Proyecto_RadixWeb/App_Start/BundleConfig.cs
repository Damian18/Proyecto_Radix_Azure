using System.Web;
using System.Web.Optimization;

namespace IdentitySample
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                   "~/Content/css/bootstrap.min.css",
                   "~/Content/css/font-awesome.min.css",
                         "~/Content/css/dashboard/app.css",
                   "~/Content/vendor/datatables/dataTables.bootstrap4.css",
                   "~/Content/css/sb-admin.css",
                   "~/Content/estilo.css",
                   "~/Content/estilosRegistrar.css",
                   "~/Content/font-awesome.css"

                   ));


            bundles.Add(new StyleBundle("~/bundles/js").Include(
                "~/Scripts/bootstrap.bundle.min.js",
                "~/Scripts/dashboard/app.js",
                  "~/Scripts/dashboard/vendor.js",
                    "~/Scripts/dashboard/sucursales.js"
 
                  ));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/bootstrap-datepicker.js"));

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
        }
    }
}
