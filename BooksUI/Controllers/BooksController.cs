using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksUI.Controllers
{
    public class BooksController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult GetAll()
        {
            return Json(unitOfWork.BookRepository.Get());
        }
	}
}