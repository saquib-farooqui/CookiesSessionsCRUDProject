using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication33.Models
{
    public class BookViewModel
    {
        public IEnumerable<Books> listofbooks { get; set; }
        public Books addBook { get; set; } = new Books();
    }
}