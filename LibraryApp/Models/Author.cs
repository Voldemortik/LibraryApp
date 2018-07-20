
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Models
{
  public  class Author
    {
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public List<Book> AuthorBook { get; set; }
    }
}
