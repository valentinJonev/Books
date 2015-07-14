using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksUI.Controllers
{
    public interface IUnitOfWork
    {
        IGenericRepository<Book> BookRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Author> AuthorRepository { get; }

        void Save();
        void Dispose();
    }
}
