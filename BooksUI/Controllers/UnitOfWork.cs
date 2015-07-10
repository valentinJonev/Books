using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace BooksUI.Controllers
{
    class UnitOfWork
    {
        private BooksContext context = new BooksContext();
        private GenericRepository<User> userRepository;
        private GenericRepository<Book> bookRepository;


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

        public void Save()
        {
            context.SaveChanges();
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
