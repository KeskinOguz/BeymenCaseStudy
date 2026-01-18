using MediatR;
using OrderModule.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Application.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<Order>
    {
        public required string OrderNumber { get; set; }
    }
}
