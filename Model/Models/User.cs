namespace Model.Models
{
    using System;
    using System.Collections.Generic;

    public partial class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }
    }
}
