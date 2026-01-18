using MediatR;
using OrderModule.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using static OrderModule.Domain.Order;

namespace OrderModule.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Order?>
    {
        public required string OrderNumber { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public List<CreateOrderItem> OrderItems { get; set; } = [];

        public class CreateOrderItem
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }
}
