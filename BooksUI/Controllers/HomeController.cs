using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksUI.Controllers
{
    public partial class HomeController : Controller
    {
        //
        // GET: /Home/
        public virtual ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            return RedirectToAction(MVC.User.Login());
        }
	}
}