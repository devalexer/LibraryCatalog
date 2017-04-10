using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryCatalog.Models
{
    public class TransactionInfo
    {
        public int Id { get; set; }
        public bool? IsCheckedOut { get; set; }
        public DateTime? LastCheckedOutDate { get; set; }
        public DateTime? DueBackDate { get; set; }
        public string FriendlyDueBackDate
        {   get
            {
                if (DueBackDate.HasValue)
                   return ((DateTime)this.DueBackDate).ToShortDateString();
                else
                   return null;
            }
        }
        
        //Tells User Current Book Check-in/Check-out Status
        //[HttpGet]
        //public IEnumerable<BookInfo> CheckedOutOrIn(int id)
        //{
        //    var rv = new List<BookInfo>();
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        var query = $@"SELECT * FROM Books WHERE Id={id}";
        //        var cmd = new SqlCommand(query, connection);
        //        connection.Open();
        //        var reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            rv.Add(new BookInfo
        //            {
        //                Id = (int)reader["Id"],
        //                Title = reader["Title"].ToString(),
        //                Author = reader["Author"].ToString(),
        //                YearPublished = (int?)reader["YearPublished"],
        //                Genre = reader["Genre"].ToString(),
        //            });
        //        }
        //        connection.Close();
        //    }
        //    return rv;
        //}

        //[HttpDelete]
        //public IHttpActionResult DeleteBook(int id)
        //{
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        var query = $@"DELETE FROM[dbo].[Books] WHERE Id = {id}";
        //        var cmd = new SqlCommand(query, connection);
        //        connection.Open();
        //        cmd.ExecuteNonQuery();
        //        connection.Close();

        //        return Ok($"Book with Id {id} has been deleted from the catalog");
        //    }
        //}

    }
}