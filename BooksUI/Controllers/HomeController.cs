namespace BooksUI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            if (this.Session["User"] != null)
            {
                return this.View();
            }

            return this.RedirectToAction(MVC.User.Login());
        }
    }
}