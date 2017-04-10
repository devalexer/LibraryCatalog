using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibraryCatalog.Models;

namespace LibraryCatalog.Controllers
{
    public class TransactionController : ApiController
    {
        const string ConnectionString = @"Server=localhost\SQLEXPRESS;Database=BookCollection;Trusted_Connection=True;";


        //Checks Out Book
        [HttpPost]
        public IHttpActionResult CheckOutBook(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = @"SELECT IsCheckedOut, DueBackDate 
                            FROM [dbo].[Books] 
                              WHERE Id = @Id";

                var cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();
                var bookInfo = new BookInfo();
                while (reader.Read())
                {
                    bookInfo = new BookInfo
                    {
                        DueBackDate = reader["DueBackDate"] as DateTime?,
                        IsCheckedOut = reader["IsCheckedOut"] as bool?
                    };
                }
                connection.Close();
                if (bookInfo.IsCheckedOut.GetValueOrDefault())
                {
                    return Ok($"This book is already checked out. It is due back on {bookInfo.DueBackDate}.");
                }
                else
                {
                    var query2 = @"UPDATE [dbo].[Books] 
                                SET [IsCheckedOut] = @IsCheckedOut, [DueBackDate] = @DueBackDate                  
                                WHERE Id = @Id";

                    var cmd2 = new SqlCommand(query2, connection);
                    cmd2.Parameters.AddWithValue("@Id", id);
                    cmd2.Parameters.AddWithValue("@IsCheckedOut", true);
                    cmd2.Parameters.AddWithValue("@DueBackDate", DateTime.Now.AddDays(10));
                    connection.Open();
                    cmd2.ExecuteNonQuery();
                    connection.Close();
                    
                    return Ok($"This book was successfully checked out. It is due back on {bookInfo.DueBackDate}.");
                }
            }
        }
           
        //Checks In Book
        [HttpPut]
        public IHttpActionResult CheckInBook(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = @"SELECT IsCheckedOut FROM [Books] WHERE Id = @Id";

                var cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();
                var bookInfo = new BookInfo();
                while (reader.Read())
                {
                    bookInfo = new BookInfo
                    {
                        IsCheckedOut = reader["IsCheckedOut"] as bool?
                    };
                }
                connection.Close();
                if (!bookInfo.IsCheckedOut.GetValueOrDefault())
                {
                    return Ok($"This book is already checked in.");
                }
                else
                {
                    var query2 = @"UPDATE [dbo].[Books] 
                                SET [IsCheckedOut] = @IsCheckedOut                  
                                WHERE Id = @Id";

                    var cmd2 = new SqlCommand(query2, connection);
                    cmd2.Parameters.AddWithValue("@Id", id);
                    cmd2.Parameters.AddWithValue("@IsCheckedOut", true);
                    connection.Open();
                    cmd2.ExecuteNonQuery();
                    connection.Close();

                    return Ok($"This book was successfully checked back in. Thank you!");
                }
            }
        }
    }
}
