using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public int PublishDate { get; set; }
        public int AuthorId { get; set; }
    }
}
