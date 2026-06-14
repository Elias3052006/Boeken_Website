using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataLayer.DataBaseContext;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.Interfaces;

namespace DataLayer.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext _context;

        public BookRepository(DatabaseContext context)
        {
            _context = context;
        }

        public bool AddBook(Book book)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = @"INSERT INTO book (Titel, Description, Price, Is_available, Author, Category_Id, Stock)
                                 VALUES (@Title, @Description, @Price, @Is_Available, @Author, @CategoryId, @Stock)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@Description", book.Description);
                    cmd.Parameters.AddWithValue("@Price", book.Price);
                    cmd.Parameters.AddWithValue("@Is_Available", book.Is_Available);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@CategoryId", book.CategoryId);
                    cmd.Parameters.AddWithValue("@Stock", book.Stock);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Book GetBookById(int bookId)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = @"SELECT idBook, Titel, Description, Price, Is_available, Author, Category_Id, Stock
                                 FROM book WHERE idBook = @BookId";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string t;
                            if (reader.IsDBNull(reader.GetOrdinal("Titel"))) 
                            { 
                                t = "";
                            }
                            else 
                            {
                                t = reader.GetString("Titel");
                            }

                            string d;
                            if (reader.IsDBNull(reader.GetOrdinal("Description"))) 
                            { 
                                d = "";
                            }
                            else
                            { 
                                d = reader.GetString("Description"); 
                            }

                            decimal p;
                            if (reader.IsDBNull(reader.GetOrdinal("Price")))
                            { 
                                p = 0;
                            }
                            else 
                            {
                                p = reader.GetDecimal("Price"); 
                            }

                            string a;
                            if (reader.IsDBNull(reader.GetOrdinal("Author"))) 
                            { 
                                a = ""; } 
                            else 
                            {
                                a = reader.GetString("Author");
                            }

                            int c = reader.GetInt32("Category_Id");

                            int s;
                            if (reader.IsDBNull(reader.GetOrdinal("Stock")))
                            { 
                                s = 0;
                            } else 
                            {
                                s = reader.GetInt32("Stock");
                            }

                            var book = new Book(t, d, p, a, c, s);

                            book.SetBookId(reader.GetInt32("idBook"));
                            return book;
                        }
                    }
                }
            }

            return null;
        }

        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = @"SELECT idBook, Titel, Description, Price, Is_available, Author, Category_Id, Stock
                                 FROM book";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string t;
                        if (reader.IsDBNull(reader.GetOrdinal("Titel"))) { t = ""; } else { t = reader.GetString("Titel"); }

                        string d;
                        if (reader.IsDBNull(reader.GetOrdinal("Description"))) { d = ""; } else { d = reader.GetString("Description"); }

                        decimal p;
                        if (reader.IsDBNull(reader.GetOrdinal("Price"))) { p = 0; } else { p = reader.GetDecimal("Price"); }

                        string a;
                        if (reader.IsDBNull(reader.GetOrdinal("Author"))) { a = ""; } else { a = reader.GetString("Author"); }

                        int c = reader.GetInt32("Category_Id");

                        int s;
                        if (reader.IsDBNull(reader.GetOrdinal("Stock"))) { s = 0; } else { s = reader.GetInt32("Stock"); }

                        var book = new Book(t, d, p, a, c, s);

                        book.SetBookId(reader.GetInt32("idBook"));
                        books.Add(book);
                    }
                }
            }

            return books;
        }

        public bool UpdateBook(Book book)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = @"UPDATE book
                                 SET Titel        = @Title,
                                     Description  = @Description,
                                     Price        = @Price,
                                     Is_available = @Is_Available,
                                     Author       = @Author,
                                     Category_Id  = @CategoryId,
                                     Stock        = @Stock
                                 WHERE idBook = @BookId";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@BookId", book.BookId);
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@Description", book.Description);
                    cmd.Parameters.AddWithValue("@Price", book.Price);
                    cmd.Parameters.AddWithValue("@Is_Available", book.Is_Available);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@CategoryId", book.CategoryId);
                    cmd.Parameters.AddWithValue("@Stock", book.Stock);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        
        
    }
}