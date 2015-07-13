using Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksUI.Controllers
{
    public class BooksController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        
        [HttpPost]
        public ActionResult Index(string Title, string Author)
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
        public ActionResult UploadBook(HttpPostedFileBase Cover, string Name, string Date, string Author)
        {
            string path = null;
            if (Cover != null && Cover.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Cover.FileName);
                var savePath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                Cover.SaveAs(savePath);
                path = "/Images/" + fileName;
            }

            Book book = new Book { Cover = path, Name = Name, AuthorId = int.Parse(Author), PublishDate = DateTime.Parse(Date) };
            unitOfWork.BookRepository.Insert(book);
            unitOfWork.Save();
           
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Delete(string id)
        {
            var book = unitOfWork.BookRepository.Get(b => b.Name == id);
            if (book.Count() > 0)
            {
                unitOfWork.BookRepository.Delete(book.First());
                unitOfWork.Save();
            }
            return RedirectToAction("Index", "Home");
        }
	}
}