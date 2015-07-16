namespace BooksUI.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/bootstrapCSS")
                .Include("~/Content/bootstrap-theme.css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-theme.css.map")
                .Include("~/Content/bootstrap.css.map")
                .Include("~/Content/fileinput.css"));

            bundles.Add(new StyleBundle("~/bundles/dataTablesCSS")
                .Include("~/Content/jquery.dataTables.css")
                .Include("~/Content/dataTables.bootstrap.css")
                .Include("~/Content/dataTables.responsive.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/jquery-{version}.min.map"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapJS")
                .Include("~/Scripts/fileinput.js")
                .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTablesJS")
                .Include("~/Scripts/jquery.dataTables.js")
                .Include("~/Scripts/dataTables.responsive.js")
                .Include("~/Scripts/dataTables.bootstrap.js")
                .Include("~/Scripts/tableDisplay.js"));

        }
    }
}