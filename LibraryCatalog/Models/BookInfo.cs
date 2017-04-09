using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryCatalog.Models
{
    public class BookInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? YearPublished { get; set; }
        public string Genre { get; set; }
    }
}