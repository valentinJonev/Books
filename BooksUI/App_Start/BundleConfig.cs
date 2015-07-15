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
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/bootstrap-theme.min.css")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/bootstrap-theme.css.map")
                .Include("~/Content/bootstrap.css.map")
                .Include("~/Content/jquery.dataTables.min.css")
                .Include("~/Content/dataTables.bootstrap.css")
                .Include("~/Content/dataTables.responsive.css"));

            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/jquery-2.1.4.min.js")
                .Include("~/Scripts/bootstrap.min.js")
                .Include("~/Scripts/jquery.validate.min.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.min.js")
                .Include("~/Scripts/jquery-2.1.4.min.map")
                .Include("~/Scripts/jquery.dataTables.min.js")
                .Include("~/Scripts/dataTables.bootstrap.js")
                .Include("~/Scripts/dataTables.responsive.js"));
        }
    }
}