namespace BooksUI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Model.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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
            if (Title != null && Title != string.Empty)
            {
                Title = Title.Trim();
                return this.Json(this.unitOfWork.BookRepository.Get(b => b.Name.Contains(Title)).OrderBy(b => b.Name));
            }

            if (Author != null && Author != string.Empty)
            {
                Author = Author.Trim();
                var authors = this.unitOfWork.AuthorRepository.Get(a => a.Name.Contains(Author));
                List<Book> booksList = new List<Book>();
                foreach (var author in authors)
                {
                    var books = this.unitOfWork.BookRepository.Get(b => b.AuthorId == author.Id);
                    foreach (var book in books)
                    {
                        booksList.Add(book);
                    }
                }

                booksList.OrderBy(b => b.Name);
                return this.Json(booksList);
            }
            else
            {
                return this.Json(this.unitOfWork.BookRepository.Get().OrderBy(b => b.Name));
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
            if (int.TryParse(Date, out date) && date >= 0 && date < DateTime.MaxValue.Year && Name != string.Empty)
            {
                Book book = new Book { Cover = path, Name = Name, AuthorId = int.Parse(Author) + 1, PublishDate = new DateTime(date, 10, 10) };
                this.unitOfWork.BookRepository.Insert(book);
                this.unitOfWork.Save();
            }
            else
            {
                throw new HttpException(500, "Server error");
            }

            return this.RedirectToAction(MVC.Home.Index());
        }

        public virtual ActionResult Delete(string id)
        {
            Book book = this.unitOfWork.BookRepository.GetByID(int.Parse(id));
            if (book != null)
            {
                this.unitOfWork.BookRepository.Delete(book);
                this.unitOfWork.Save();
            }

            return this.RedirectToAction(MVC.Home.Index());
        }

        public virtual ActionResult Edit(string id)
        {
            if (this.Session["User"] != null)
            {
                Book book = this.unitOfWork.BookRepository.GetByID(int.Parse(id));
                if (book != null)
                {
                    ViewBag.Authors = this.unitOfWork.AuthorRepository.Get();
                    return this.View(book);
                }

                return this.View();
            }

            return this.RedirectToAction(MVC.User.Login());
        }

        [HttpPost]
        public virtual ActionResult Edit(HttpPostedFileBase Cover, string Id, string PublishDate, string AuthorId)
        {
            string path = null;
            if (Cover != null && Cover.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Cover.FileName);
                var savePath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                Cover.SaveAs(savePath);
                path = "/Images/" + fileName;
            }

            Book book = this.unitOfWork.BookRepository.GetByID(int.Parse(Id));
            if (book != null)
            {
                book.PublishDate = new DateTime(int.Parse(PublishDate), 10, 10);
                book.AuthorId = int.Parse(AuthorId);
                if (path != null)
                {
                    book.Cover = path;
                }

                this.unitOfWork.BookRepository.Update(book);
                this.unitOfWork.Save();
            }

            return this.RedirectToAction(MVC.Home.Index());
        }
    }
}