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
        const string ConnectionString = @"Server=localhost\SQLEXPRESS;Database=BookCollection;Trusted_Connection=True;";

        //Gets List Of All Books
        [HttpGet]
        public IEnumerable<BookInfo> GetAllBooks()
        {
            var rv = new List<BookInfo>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = "SELECT * FROM Books ORDER BY Title";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new BookInfo
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        YearPublished = (int?)reader["YearPublished"],
                        Genre = reader["Genre"].ToString(),
                    });
                }
                connection.Close();
            }
            return rv;
        }

        //Gets Individual Book by Id number
        [HttpGet]
        public IEnumerable<BookInfo> GetBook(int id)
        {
            var rv = new List<BookInfo>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = $@"SELECT * FROM Books WHERE Id={id}";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new BookInfo
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        YearPublished = (int?)reader["YearPublished"],
                        Genre = reader["Genre"].ToString(),
                    });
                }
                connection.Close();
            }
            return rv;
        }

        //Creates New Book And Adds It To Catalog
        [HttpPut]
        public IHttpActionResult CreateBook([FromBody]BookInfo book)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = @"INSERT INTO[dbo].[Books] ([Title],[Author],[YearPublished],[Genre])
                              OUTPUT INSERTED.Id                          
                              VALUES (@Title, @Author, @YearPublished, @Genre)";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                var newId = cmd.ExecuteScalar();
                book.Id = (int)newId;
                connection.Close();
            }
            return Ok(book);
        }

        //Updates Existing Book In Catalog
        [HttpPost]
        public IHttpActionResult UpdateBook([FromUri]int id, [FromBody] BookInfo book)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = @"INSERT INTO[dbo].[Books] ([Title],[Author],[YearPublished],[Genre])
                              OUTPUT INSERTED.Id                          
                              VALUES (@Title, @Author, @YearPublished, @Genre)";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                var newId = cmd.ExecuteScalar();
                book.Id = (int)newId;
                connection.Close();
            }
            return Ok(book);
        }

        //Deletes Book From Catalog
        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = $@"DELETE FROM[dbo].[Books] WHERE Id = {id}";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return Ok($"Book with Id '{id}' has been deleted from the catalog");
            }
        }

        //WORKING....

            //[HttpGet]
            //public IHttpActionResult GetBook(int id)
            //{
            //    var book = BookInfo.FirstOrDefault(f => f.Id == id);
            //    if (book == null)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        return Ok(book);
            //    }
            //}

            //[HttpPut]
            //public IHttpActionResult AddBook([FromBody] BookInfo book)
            //{
            //    var newId = BookInfo.Max(m => m.Id);
            //    if (book != null && !String.IsNullOrEmpty(book.Title))
            //    {
            //        book.Id = newId++;
            //        BookInfo.Add(book);
            //        return Ok(book);
            //    }
            //    else
            //    {
            //        return Ok(new { Message = "No book was given, nothing changed" });
            //    }
            //}

            //[HttpPost]
            //public IHttpActionResult UpdateBook([FromUri]int id, [FromBody] BookInfo book)
            //{
            //    var oldBook = BookInfo.FirstOrDefault(f => f.Id == id);
            //    if (oldBook == null)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        BookInfo.Remove(oldBook);
            //        BookInfo.Add(book);
            //        return Ok(book);
            //    }
            //}

            //[HttpDelete]
            //public IHttpActionResult DeleteBook(int id)
            //{
            //    var oldBook = BookInfo.FirstOrDefault(b => b.Id == id);
            //    if (oldBook == null)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        BookInfo.Remove(oldBook);
            //        return Ok();
            //    }
            //}
        }
    }
