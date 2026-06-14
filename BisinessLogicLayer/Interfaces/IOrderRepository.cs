using System;
using System.Collections.Generic;
using BisinessLogicLayer.Domain;

namespace BisinessLogicLayer.Interfaces
{
    public interface IOrderRepository
    {
        int CreateOrder(Order order);
        List<Order> GetOrdersByUser(int userId);
    }
}
