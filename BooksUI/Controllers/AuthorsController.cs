namespace BooksUI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

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
            return this.Json(this.unitOfWork.AuthorRepository.Get());
        }
    }
}