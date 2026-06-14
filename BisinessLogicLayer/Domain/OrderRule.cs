using System;

namespace BisinessLogicLayer.Domain
{
    public class OrderRule
    {
        public int OrderId { get; private set; }
        public int BookId { get; private set; }
        public decimal TotalPrice { get; private set; }
        public int Quantity { get; private set; }

        
        public OrderRule(int orderId, int bookId, decimal totalPrice, int quantity)
        {
            if (bookId <= 0)
            {
                throw new ArgumentException("Boek is niet geldig!");
            }
            if (totalPrice <= 0)
            {
                throw new ArgumentException("Totaalprijs moet groter zijn dan 0!");
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Aantal moet groter zijn dan 0!");
            }

            OrderId = orderId;
            BookId = bookId;
            TotalPrice = totalPrice;
            Quantity = quantity;
        }

        
        public void SetOrderId(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Ongeldig Order ID.");
            }
            OrderId = orderId;
        }
    }
}