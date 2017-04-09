using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryCatalog.Models
{
    public class CheckOutInfo
    {
        public bool IsCheckedOut { get; set; }
        public DateTime LastCheckedOutDate { get; set; }
        public DateTime DueBackDate { get; set; }
    }
}