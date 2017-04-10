using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryCatalog.Models
{
    public class BookInfo
    {
        //public BookInfo(SqlDataReader reader)
        //{
        //    this.Id = (int)reader["Id"];
        //    this.Title = reader["Title"].ToString();
        //}

        //public BookInfo(string title, int id)
        //{
        //    this.Title = Title;
        //    this.Id = id;
        //}

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? YearPublished { get; set; }
        public string Genre { get; set; }
    }
}