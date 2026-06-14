using System;
using System.Collections.Generic;

namespace BisinessLogicLayer.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Is_Deliverd { get; set; }
        public int UserId { get; set; }
        public List<OrderRuleDTO> OrderRules { get; set; } = new List<OrderRuleDTO>();
    }
}
