namespace BooksUI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity.Validation;
    using System.Data.Entity;
    using Model.Models;

    public class UnitOfWork : IUnitOfWork
    {
        private BooksContext context = new BooksContext();
        private IGenericRepository<User> userRepository;
        private IGenericRepository<Book> bookRepository;
        private IGenericRepository<Author> authorRepository;
        private bool disposed = false;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IGenericRepository<Book> BookRepository
        {
            get 
            {
                if (this.bookRepository == null)
                {
                    this.bookRepository = new GenericRepository<Book>(this.context);
                }

                return this.bookRepository; 
            }
        }
        
        public IGenericRepository<User> UserRepository
        {
            get 
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(this.context);   
                }

                return this.userRepository; 
            }
        }

        public IGenericRepository<Author> AuthorRepository
        {
            get
            {
                if (this.authorRepository == null)
                {
                    this.authorRepository = new GenericRepository<Author>(this.context);
                }

                return this.authorRepository;
            }
        }

        public void Save()
        {
            try
            {
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                log.Error("An error occured in UnitOfWork (Save) : ", e);
                throw;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        this.context.Dispose();
                    }
                }

                this.disposed = true;
            }
            catch (Exception e)
            {
                log.Error("An error occured in UnitOfWork (Dispose) : ", e);
                throw;
            }
            
        }
    }
}
