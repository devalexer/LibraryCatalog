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
            new BookInfo { Title = "Where the Sidewalk Ends", Author = "Roald Dahl", YearPublished = 1979, Genre = "Childrens", IsCheckedOut = false }
        };

        const string ConnectionString = @"Server=localhost\SQLEXPRESS;Database=BookCollection;Trusted_Connection=True;";

        [HttpGet]
        public IEnumerable<BookInfo> GetAllBooks()
        {
            return BookInfo;
        }

        [HttpGet]
        public BookInfo GetBook(int title)
        {
            return new BookInfo();
        }

        [HttpPut]
        public BookInfo AddBook([FromBody] BookInfo book)
        {
            return new BookInfo();
        }

        [HttpPost]
        public BookInfo UpdateBook([FromBody] BookInfo book)
        {
            return new BookInfo();
        }

        [HttpDelete]
        public IHttpActionResult DeleteBook(int book)
        {
            return Ok();
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


        //// GET: api/Book
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Book/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Book
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Book/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Book/5
        //public void Delete(int id)
        //{
        //}
    }
}
