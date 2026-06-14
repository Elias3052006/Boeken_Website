using System;
using MySql.Data.MySqlClient;
using DataLayer.DataBaseContext;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.Interfaces;

namespace DataLayer.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseContext _context;

        public EmployeeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public string GetRole(string username, string password)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = "SELECT actor FROM employee WHERE EmployeeName = @Username AND Password = @Password";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    var result = cmd.ExecuteScalar();

                    
                    if (result == null)
                    {
                        return null;
                    }
                    else
                    {
                        return result.ToString();
                    }
                }
            }
        }

        public int GetEmployeeIdByName(string employeeName)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = "SELECT idemployee FROM employee WHERE EmployeeName = @EmployeeName";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeName", employeeName);
                    var result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
        }
    }
}