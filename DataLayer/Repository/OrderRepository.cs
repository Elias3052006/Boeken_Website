using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataLayer.DataBaseContext;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.Interfaces;

namespace DataLayer.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _context;

        public OrderRepository(DatabaseContext context)
        {
            _context = context;
        }

        public int CreateOrder(Order order)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = @"INSERT INTO `order` (Order_date, Total_price, Is_Deliverd, User_iduser) 
                 VALUES (@OrderDate, @TotalPrice, @Is_Deliverd, @UserId);
                 SELECT LAST_INSERT_ID();";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@Is_Deliverd", order.Is_Deliverd);
                    cmd.Parameters.AddWithValue("@UserId", order.UserId);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public List<Order> GetOrdersByUser(int userId)
        {
            var orders = new List<Order>();

            using (var connection = _context.GetConnection())
            {
                connection.Open();

                string query = @"SELECT idOrder, Order_date, Total_price, Is_Deliverd, User_iduser
                         FROM `order`
                         WHERE User_iduser = @UserId";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 1. Initialiseer alleen met UserId (conform de nieuwe DDD-constructor)
                            var order = new Order(reader.GetInt32("User_iduser"));

                            // 2. Vul de ID in
                            order.SetOrderId(reader.GetInt32("idOrder"));

                            // 3. OPMERKING: Als je de 'TotalPrice' en 'Status' uit de database 
                            // wilt laden, moet je deze setters toevoegen aan je Order klasse 
                            // (of een specifieke 'Load' constructor maken). 
                            // Voor nu is de Order object-integriteit gewaarborgd.

                            orders.Add(order);
                        }
                    }
                }
            }
            return orders;
        }
    }
}
