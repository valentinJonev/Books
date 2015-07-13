using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Book
    {
        public string Cover { get; set; }
        public string Name { get; set; }
        public System.DateTime PublishDate { get; set; }
        public Nullable<int> AuthorId { get; set; }
    }
}
