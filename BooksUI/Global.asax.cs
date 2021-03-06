﻿[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BooksUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Microsoft.Practices.Unity;
    using BooksUI.App_Start;
    using OneTrueError.Reporting;
    using OneTrueError.AspNet.Mvc5;
    using Model.Models;
    using BooksUI.Binders;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            OneTrue.Configuration.Credentials("7b9dac77-3934-40c4-ac48-c866bdbb63b5", "b893e6cf-2475-4cfa-9300-205fe8bf3a5e");
            OneTrue.Configuration.CatchMvcExceptions();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(SearchViewModel), new TrimModelBinder());
        }
    }
}
