using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataLayer.DataBaseContext;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.Interfaces;

namespace DataLayer.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategory()
        {
            var categories = new List<Category>();

            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = "SELECT idCategorie, Name FROM category";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var category = new Category(reader.GetString("Name"));
                        category.SetCategoryId(reader.GetInt32("idCategorie"));
                        categories.Add(category);
                    }
                }
            }

            return categories;
        }

        public List<Book> GetAllBooksByCategory(int categoryId)
        {
            var books = new List<Book>();

            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = @"SELECT idBook, Titel, Description, Price, Is_available, Author, Category_Id, Stock
                                 FROM book WHERE Category_Id = @CategoryId";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var book = new Book(
                                reader.IsDBNull(reader.GetOrdinal("Titel")) ? "" : reader.GetString("Titel"),
                                reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description"),
                                reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal("Price"),
                                reader.IsDBNull(reader.GetOrdinal("Author")) ? "" : reader.GetString("Author"),
                                reader.GetInt32("Category_Id"),
                                reader.IsDBNull(reader.GetOrdinal("Stock")) ? 0 : reader.GetInt32("Stock")
                            );

                            book.SetBookId(reader.GetInt32("idBook"));
                            books.Add(book);
                        }
                    }
                }
            }

            return books;
        }

        public bool AddCategory(Category category)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO category (Name) VALUES (@Name)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", category.CategoryName);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public Category GetCategoryById(int categoryId)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                string query = "SELECT idCategorie, Name FROM category WHERE idCategorie = @Id";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", categoryId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var cat = new Category(reader.GetString("Name"));
                            cat.SetCategoryId(reader.GetInt32("idCategorie"));
                            return cat;
                        }
                    }
                }
            }
            return null;
        }

        public bool UpdateCategory(Category category)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = "UPDATE category SET Name = @Name WHERE idCategorie = @CategoryId";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", category.CategoryName);
                    cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

   
    }
}
