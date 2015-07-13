using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using System.Data.Entity.Validation;

namespace BooksUI.Controllers
{
    class UnitOfWork
    {
        private BooksContext context = new BooksContext();
        private GenericRepository<User> userRepository;
        private GenericRepository<Book> bookRepository;
        private GenericRepository<Author> authorRepository;


        public GenericRepository<Book> BookRepository
        {
            get 
            {
                if (this.bookRepository == null)
                {
                    this.bookRepository = new GenericRepository<Book>(context);
                }
                return bookRepository; 
            }
        }
        
        public GenericRepository<User> UserRepository
        {
            get 
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);   
                }
                return userRepository; 
            }
        }

        public GenericRepository<Author> AuthorRepository
        {
            get
            {
                if (this.authorRepository == null)
                {
                    this.authorRepository = new GenericRepository<Author>(context);
                }
                return authorRepository;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
    }
}
