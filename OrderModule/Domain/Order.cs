using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Domain
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string OrderNumber { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public class OrderItem
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }

            public Guid OrderId { get; set; }
            public Order Order { get; set; } = null!;
        }

        public void CalculateTotal()
        {
            TotalPrice = OrderItems.Sum(item => item.Quantity * item.UnitPrice);
        }
    }
}
