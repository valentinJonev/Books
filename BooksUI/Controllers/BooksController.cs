using Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BooksUI.Controllers
{
    public partial class BooksController : Controller
    {
        private UnitOfWork unitOfWork;
        public BooksController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        public virtual ActionResult Index(string Title, string Author)
        {
            if (Title != null && Title != "")
            {
                Title = Title.Trim();
                return Json(unitOfWork.BookRepository.Get(b => b.Name == Title));
            }
            if (Author != null && Author != "")
            {
                Author = Author.Trim();
                var author = unitOfWork.AuthorRepository.Get(a => a.Name == Author);
                foreach (var _author in author)
                {
                    return Json(unitOfWork.BookRepository.Get(b => b.AuthorId == _author.Id));
                }
                return View();
            }
            else
            {
                return Json(unitOfWork.BookRepository.Get());
            }
            
        }

        [HttpPost]
        public virtual ActionResult UploadBook(HttpPostedFileBase Cover, string Name, string Date, string Author)
        {
            string path = null;
            if (Cover != null && Cover.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Cover.FileName);
                var savePath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                Cover.SaveAs(savePath);
                path = "/Images/" + fileName;
            }
            int date = 0;
            if (int.TryParse(Date,out date) && date >= 0 && date < DateTime.MaxValue.Year)
            {
                Random random = new Random();
                Book book = new Book { BookId = random.Next(0, 25565), Cover = path, Name = Name, AuthorId = int.Parse(Author), PublishDate = new DateTime(date, 1, 1) };
                unitOfWork.BookRepository.Insert(book);
                unitOfWork.Save();
            }
            else
            {
                throw new HttpException(500,"Out of range input");
            }

            return RedirectToAction(MVC.Home.Index());
        }
        public virtual ActionResult Delete(string id)
        {
            var book = unitOfWork.BookRepository.Get(b => b.Name == id);
            if (book.Count() > 0)
            {
                unitOfWork.BookRepository.Delete(book.First());
                unitOfWork.Save();
            }
            return RedirectToAction(MVC.Home.Index());
        }

        public virtual ActionResult Edit(string id)
        {
            if (Session["User"] != null)
            {
                var book = unitOfWork.BookRepository.Get(b => b.Name == id);
                if (book.Count() > 0)
                {
                    return View(book.First());
                }
                return View();
            }
            return RedirectToAction(MVC.User.Login());
        }

        [HttpPost]
        public virtual ActionResult Edit(HttpPostedFileBase Cover, string Name, string PublishDate, string AuthorId)
        {
            string path = null;
            if (Cover != null && Cover.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Cover.FileName);
                var savePath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                Cover.SaveAs(savePath);
                path = "/Images/" + fileName;
            }
            var book = unitOfWork.BookRepository.Get(b => b.Name == Name);
            if (book.Count() > 0)
            {
                Book b = book.First();
                b.PublishDate = DateTime.Parse(PublishDate);
                b.AuthorId = int.Parse(AuthorId);
                if (path != null)
                {
                    b.Cover = path;
                }
                unitOfWork.BookRepository.Update(b);
                unitOfWork.Save();
            }

            return RedirectToAction(MVC.Home.Index());
        }
	}
}