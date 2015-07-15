namespace Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDbContext
    {
        DbSet<Author> Authors { get; set; }

        DbSet<Book> Books { get; set; }

        DbSet<User> Users { get; set; }
    }
}
