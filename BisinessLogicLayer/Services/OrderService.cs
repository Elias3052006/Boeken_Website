using System;
using System.Collections.Generic;
using System.Linq;
using BisinessLogicLayer.Domain;
using BisinessLogicLayer.DTOs;
using BisinessLogicLayer.Interfaces;

namespace BisinessLogicLayer.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderRuleRepository _orderRuleRepository;
        private readonly IBookRepository _bookRepository;

        public OrderService(IOrderRepository orderRepository, IOrderRuleRepository orderRuleRepository, IBookRepository bookRepository)
        {
            _orderRepository = orderRepository;
            _orderRuleRepository = orderRuleRepository;
            _bookRepository = bookRepository;
        }

        public bool CreateOrder(OrderDTO dto)
        {
            
            var order = new Order(dto.UserId);

            
            foreach (var ruleDto in dto.OrderRules)
            {
                var book = _bookRepository.GetBookById(ruleDto.BookId);

                if (book == null) return false;

                
                try
                {
                    order.AddBook(book, ruleDto.Quantity);
                }
                catch (InvalidOperationException)
                {
                    
                    return false;
                }
            }

            
            int orderId = _orderRepository.CreateOrder(order);

            if (orderId <= 0) return false;

            order.SetOrderId(orderId);

            
            foreach (var rule in order.OrderRules)
            {
                _orderRuleRepository.AddOrderRule(rule);

                var book = _bookRepository.GetBookById(rule.BookId);
                book.ReduceStock(rule.Quantity);
                _bookRepository.UpdateBook(book);
            }

            return true;
        }

        public List<OrderDTO> GetOrdersByUser(int userId)
        {
            var orders = _orderRepository.GetOrdersByUser(userId);

            return orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Is_Deliverd = o.Is_Deliverd,
                UserId = o.UserId
            }).ToList();
        }
    }
}