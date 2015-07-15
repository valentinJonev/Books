namespace Model.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Book
    {
        public int BookId { get; set; }

        public string Name { get; set; }

        public string Cover { get; set; }

        public System.DateTime PublishDate { get; set; }

        public int AuthorId { get; set; }
    }
}
