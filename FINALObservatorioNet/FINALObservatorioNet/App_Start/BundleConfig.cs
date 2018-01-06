using System.Web;
using System.Web.Optimization;

namespace FINALObservatorioNet
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css/css").Include(
                      "~/Content/css/normalize.css",
                      "~/Content/css/grid-bootstrap.css",
                      "~/Content/css/deboot.css",
                      "~/Content/css/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Jquery").Include(
                      "~/Content/lib/jquery-3.2.1.min.js",
                      "~/Content/lib/jquery.loader.min.js",
                      "~/Content/lib/d3.v3.min.js"));

            

            BundleTable.EnableOptimizations = true;
        }
    }
}
