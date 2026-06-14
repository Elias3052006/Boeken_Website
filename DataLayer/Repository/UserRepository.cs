using System;
using MySql.Data.MySqlClient;
using DataLayer.DataBaseContext;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.Interfaces;

namespace DataLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

       
        public UserAccount GetUserByUsername(string username)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                string query = "SELECT iduser, Username, Password FROM user WHERE Username = @Username";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserAccount(
                                Convert.ToInt32(reader["iduser"]),
                                reader["Username"].ToString(),
                                reader["Password"].ToString()
                            );
                        }
                    }
                }
            }
            return null;
        }


        public string GetRole(string username, string password)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                string query = "SELECT actor FROM user WHERE Username = @Username AND Password = @Password";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    var result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        public bool SignUp(User user)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO user (Username, Password, Email, Postcode, House_number, actor)
                                 VALUES (@Username, @Password, @Email, @PostCode, @HouseNumber, 'user')";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PostCode", user.PostCode);
                    cmd.Parameters.AddWithValue("@HouseNumber", user.HouseNumber);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public int GetUserIdByUsername(string username)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                string query = "SELECT iduser FROM user WHERE Username = @Username";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public User GetByUsername(string username)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM user WHERE Username = @Username";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var user = new User(
                                reader["Username"].ToString(),
                                reader["Password"].ToString(),
                                reader["Email"].ToString(),
                                reader["PostCode"].ToString(),
                                Convert.ToInt32(reader["HouseNumber"])
                            );
                            user.SetUserId(Convert.ToInt32(reader["iduser"]));
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public bool UsernameExists(string username)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM user WHERE Username = @Username";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }
    }
}