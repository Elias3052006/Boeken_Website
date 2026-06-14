using System.Collections.Generic;
using BisinessLogicLayer.DTOs;

namespace ViewLayer.Models
{
    public class OrderBookItem
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsSelected { get; set; }
    }

    public class OrderViewModel
    {
        public int UserId { get; set; }
        public List<OrderBookItem> Books { get; set; } = new List<OrderBookItem>();
    }
}
