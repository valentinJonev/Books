using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksUI.Controllers
{
    public partial class AuthorsController : Controller
    {
        private UnitOfWork unitOfWork;
        public AuthorsController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

         [HttpPost]
        public virtual ActionResult Index()
        {
            return Json(unitOfWork.AuthorRepository.Get());
        }
	}
}