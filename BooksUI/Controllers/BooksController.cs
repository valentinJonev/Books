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
    using BooksUI.Binders;

    public partial class BooksController : Controller
    {
        private UnitOfWork unitOfWork;

        public BooksController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public virtual ActionResult Index([ModelBinder(typeof(TrimModelBinder))] SearchViewModel search)
        {
            if (this.Session["User"] == null)
            {
                return this.RedirectToAction(MVC.User.Login());
            }

            List<SelectListItem> authorsView = new List<SelectListItem>();
            var authors = this.unitOfWork.AuthorRepository.Get();
            foreach (Author author in authors)
            {
                authorsView.Add(new SelectListItem { Text = author.Name, Value = author.Id.ToString() });
            }
            
            ViewBag.Authors = authorsView;

            IEnumerable<Author> authorsList;
            IEnumerable<Book> booksList;
            List<BookViewModel> booksView = new List<BookViewModel>();
            if (!string.IsNullOrEmpty(search.Author))
            {
                authorsList = this.unitOfWork.AuthorRepository.Get(a => a.Name.Contains(search.Author));
            }
            else
            {
                authorsList = this.unitOfWork.AuthorRepository.Get();
            }

            if (!string.IsNullOrEmpty(search.Title))
            {
                foreach (Author author in authorsList)
                {
                    booksList = this.unitOfWork.BookRepository.Get(b => b.AuthorId == author.Id && b.Name.Contains(search.Title));
                    foreach (var book in booksList)
                    {
                        booksView.Add(new BookViewModel { BookId = book.BookId, Author = author.Name, Cover = book.Cover, Name = book.Name, PublishDate = book.PublishDate });
                    }
                }
            }
            else 
            {
                foreach (Author author in authorsList)
                {
                    booksList = this.unitOfWork.BookRepository.Get(b => b.AuthorId == author.Id);
                    foreach (var book in booksList)
                    {
                        booksView.Add(new BookViewModel { BookId = book.BookId, Author = author.Name, Cover = book.Cover, Name = book.Name, PublishDate = book.PublishDate });
                    }
                }
            }
            
            return this.View(booksView.OrderBy(b => b.Name));
        }

        [HttpPost]
        public virtual ActionResult UploadBook(HttpPostedFileBase Cover, string Name, string Date, string Authors)
        {
            try
            {
                string path = "http://i.imgur.com/sJ3CT4V.gif";
                if (Cover != null && Cover.ContentLength > 0)
                {
                    if (Path.GetExtension(Cover.FileName) != ".jpg" && Path.GetExtension(Cover.FileName) != ".jpeg" && Path.GetExtension(Cover.FileName) != ".png" && Path.GetExtension(Cover.FileName) != ".gif")
                    {
                        throw new HttpException(500, "File extension error");
                    }
                    
                    var fileName = Path.GetFileName(Cover.FileName);
                    var savePath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    Cover.SaveAs(savePath);
                    path = "/Images/" + fileName;
                }

                int date = 0;
                if (int.TryParse(Date, out date) && date >= 0 && date < DateTime.MaxValue.Year && Name != string.Empty)
                {
                    Book book = new Book { Cover = path, Name = Name, AuthorId = int.Parse(Authors), PublishDate = date };
                    this.unitOfWork.BookRepository.Insert(book);
                    this.unitOfWork.Save();
                }
                else
                {
                    throw new HttpException(500, "Out of range argument error");
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                throw;
            }

            return this.RedirectToAction(MVC.Home.Index());
        }

        public virtual ActionResult Delete(string id)
        {
            try
            {
                Book book = this.unitOfWork.BookRepository.GetByID(int.Parse(id));
                if (book != null)
                {
                    this.unitOfWork.BookRepository.Delete(book);
                    this.unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                throw;
            }
            
            return this.RedirectToAction(MVC.Home.Index());
        }

        public virtual ActionResult Edit(string id)
        {
            try
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
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                throw;
            }
            
            return this.RedirectToAction(MVC.User.Login());
        }

        [HttpPost]
        public virtual ActionResult Edit(HttpPostedFileBase Cover, string Id, string PublishDate, string AuthorId)
        {
            try
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
                    book.PublishDate = int.Parse(PublishDate);
                    book.AuthorId = int.Parse(AuthorId);
                    if (path != null)
                    {
                        book.Cover = path;
                    }

                    this.unitOfWork.BookRepository.Update(book);
                    this.unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                throw;
            }
            
            return this.RedirectToAction(MVC.Home.Index());
        }
    }
}