using System;
using System.Collections.Generic;
using System.Linq;

namespace BisinessLogicLayer.Domain
{
    public class Order
    {
        public int OrderId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string Is_Deliverd { get; private set; }
        public int UserId { get; private set; }

        private readonly List<OrderRule> _orderRules = new List<OrderRule>();
        public IReadOnlyCollection<OrderRule> OrderRules => _orderRules.AsReadOnly();

        public Order(int userId)
        {
            if (userId <= 0) throw new ArgumentException("Gebruiker ID moet geldig zijn.");

            UserId = userId;
            OrderDate = DateTime.Now;
            Is_Deliverd = "Pending";
            TotalPrice = 0;
        }

        
        public void AddBook(Book book, int quantity)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            
            if (book.IsInStock(quantity) == false)
            {
                throw new InvalidOperationException($"Boek '{book.Title}' is niet op voorraad.");
            }

            
            var rule = new OrderRule(0, book.BookId, book.Price * quantity, quantity);
            _orderRules.Add(rule);


            TotalPrice = TotalPrice + rule.TotalPrice;
        }

        public void SetOrderId(int orderId)
        {
            if (orderId <= 0) throw new ArgumentException("Ongeldig Order ID.");
            OrderId = orderId;
        }

        public void UpdateStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) throw new ArgumentException("Status mag niet leeg zijn.");
            Is_Deliverd = status;
        }
    }
}