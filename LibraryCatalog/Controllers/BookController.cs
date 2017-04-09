using LibraryCatalog.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryCatalog.Controllers
{
    public class BookController : ApiController
    {

        static private List<BookInfo> BookInfo { get; set; } = new List<BookInfo>
        {
            new BookInfo { Id = 1, Title = "Where the Sidewalk Ends", Author = "Roald Dahl", YearPublished = 1979, Genre = "Childrens", IsCheckedOut = false },
            new BookInfo { Id = 2, Title = "Never Without You", Author = "Dunno", YearPublished = 1979, Genre = "Childrens", IsCheckedOut = false },
            new BookInfo { Id = 3, Title = "Goodnight Moon", Author = "Diff Person", YearPublished = 1979, Genre = "Childrens", IsCheckedOut = false }
        };

        const string ConnectionString = @"Server=localhost\SQLEXPRESS;Database=BookCollection;Trusted_Connection=True;";

        [HttpGet]
        public IEnumerable<BookInfo> GetAllBooks()
        {
            return BookInfo;
        }

        [HttpGet]
        public IHttpActionResult GetBook(int id)
        {
            var book = BookInfo.FirstOrDefault(f => f.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
            return Ok(book);
            }
        }

        [HttpPut]
        public IHttpActionResult AddBook([FromBody] BookInfo book)
        {
            var newId = BookInfo.Max(m => m.Id);
            if (book != null && !String.IsNullOrEmpty(book.Title))
            {
                book.Id = newId++;
                BookInfo.Add(book);
                return Ok(book);
            }
            else
            {
                return Ok(new { Message = "No book was given, nothing changed" });
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateBook([FromUri]int id, [FromBody] BookInfo book)
        {
            var oldBook = BookInfo.FirstOrDefault(f => f.Id == id);
            if (oldBook == null)
            {
                return NotFound();
            }
            else
            {
                BookInfo.Remove(oldBook);
                BookInfo.Add(book);
                return Ok(book);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            var oldBook = BookInfo.FirstOrDefault(b => b.Id == id);
            if (oldBook == null)
            {
                return NotFound();
            }
            else
            {
                BookInfo.Remove(oldBook);
                return Ok();
            }
        }

      
        //[HttpGet]
        //public IEnumerable<BookInfo> GetAllBooks()
        //{
        //    var rv = new List<BookInfo>();
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        var query = "SELECT * FROM Books";
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
        //                YearPublished = (int)reader["YearPublished"],
        //                Genre = reader["Genre"].ToString(),
        //                IsCheckedOut = (bool)reader["IsCheckedOut"],
        //                LastCheckedOutDate = (DateTime)reader["LastCheckedOutDate"],
        //                DueBackDate = (DateTime)reader["DueBackDate"],
        //            });
        //        }

        //        connection.Close();
        //    }
        //    return rv;
        //}
        
    }
}
