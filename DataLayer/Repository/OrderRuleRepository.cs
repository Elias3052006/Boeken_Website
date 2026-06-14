using System;
using MySql.Data.MySqlClient;
using DataLayer.DataBaseContext;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.Interfaces;

namespace DataLayer.Repository
{
    public class OrderRuleRepository : IOrderRuleRepository
    {
        private readonly DatabaseContext _context;

        public OrderRuleRepository(DatabaseContext context)
        {
            _context = context;
        }

        public bool AddOrderRule(OrderRule orderRule)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = @"INSERT INTO order_rule (order_idOrder, book_idBook, Total_price, Quantity) 
                 VALUES (@OrderId, @BookId, @TotalPrice, @Quantity)"; 

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderRule.OrderId);
                    cmd.Parameters.AddWithValue("@BookId", orderRule.BookId);
                    cmd.Parameters.AddWithValue("@TotalPrice", orderRule.TotalPrice);
                    cmd.Parameters.AddWithValue("@Quantity", orderRule.Quantity); 

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
