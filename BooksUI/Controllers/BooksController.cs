﻿namespace BooksUI.Controllers
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

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BooksController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        public virtual ActionResult Index([ModelBinder(typeof(CustomBinder))] SearchViewModel search)
        {
            if (!string.IsNullOrEmpty(search.Title))
            {
                if (!string.IsNullOrEmpty(search.Author))
                {
                    try
                    {
                        var authors = this.unitOfWork.AuthorRepository.Get(a => a.Name.Contains(search.Author));
                        List<Book> booksList = new List<Book>();
                        foreach (var author in authors)
                        {
                            var books = this.unitOfWork.BookRepository.Get(b => b.AuthorId == author.Id && b.Name.Contains(search.Title));
                            foreach (var book in books)
                            {
                                booksList.Add(book);
                            }
                        }

                        booksList.OrderBy(b => b.Name);
                        return this.Json(booksList);
                    }
                    catch (Exception ex)
                    {
                        log.Error("An error occured in Books/Index (Search by title and author) : ", ex);
                        throw;
                    }
                    
                }
                else
                {
                    return this.Json(this.unitOfWork.BookRepository.Get(b => b.Name.Contains(search.Title)).OrderBy(b => b.Name));
                }
                
            }

            if (!string.IsNullOrEmpty(search.Author))
            {
                try
                {
                    var authors = this.unitOfWork.AuthorRepository.Get(a => a.Name.Contains(search.Author));
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
                catch (Exception ex)
                {
                    log.Error("An error occured in Books/Index (Search by author) : ", ex);
                    throw;
                }
                
            }
            else
            {
                return this.Json(this.unitOfWork.BookRepository.Get().OrderBy(b => b.Name));
            }
        }

        [HttpPost]
        public virtual ActionResult UploadBook(HttpPostedFileBase Cover, string Name, string Date, string Author)
        {
            try
            {
                if (Path.GetExtension(Cover.FileName) != ".jpg" && Path.GetExtension(Cover.FileName) != ".jpeg" && Path.GetExtension(Cover.FileName) != ".png" && Path.GetExtension(Cover.FileName) != ".gif")
                {
                    throw new HttpException(500, "File extension error");
                }
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
                    Book book = new Book { Cover = path, Name = Name, AuthorId = int.Parse(Author) + 1, PublishDate = date };
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
                log.Error("An error occured in Books/UploadBook : ", ex);
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
                log.Error("An error occured in Books/Delete : ", ex);
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
                log.Error("An error occured in Books/Edit (HttpGet) : ", ex);
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
                log.Error("An error occured in Books/Edit (HttpPost) : ", ex);
                throw;
            }
            
            return this.RedirectToAction(MVC.Home.Index());
        }
    }
}