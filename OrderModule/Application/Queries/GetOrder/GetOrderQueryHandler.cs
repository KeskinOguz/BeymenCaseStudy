using MediatR;
using OrderModule.Domain;
using OrderModule.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Application.Queries.GetOrder
{
    public class GetOrderQueryHandler  : IRequestHandler<GetOrderQuery, Order?>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return  await _orderRepository.GeOrderWithOrderItemsByOrderNumber(request.OrderNumber);
        }
    }
}
