using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModule.Events
{
    public record OrderCreatedEvent : IIntegrationEvent
    {
        public Guid OrderId { get; init; }
        public required string OrderNumber { get; set; }
        public List<OrderCreatedItem> OrderItems { get; init; } = [];
        public decimal TotalAmount { get; init; }
        public string? CustomerEmail { get; init; }
        public Guid Id => Guid.NewGuid();
        public DateTime CreatedDate => DateTime.UtcNow;

        public class OrderCreatedItem
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }

}
