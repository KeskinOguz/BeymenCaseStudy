using CommonModule;
using CommonModule.Events;
using CommonModule.Models;
using CommonModule.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderModule.Domain;
using OrderModule.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Application.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order?>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly RabbitMQPublisher _publisher;
        private readonly IStockService _stockService;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, RabbitMQPublisher publisher,IStockService stockService, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _publisher = publisher;
            _stockService = stockService;
            _logger = logger;
        }

        public async Task<Order?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                OrderNumber = request.OrderNumber,
                CustomerEmail = request.CustomerEmail,
                OrderItems = request.OrderItems.ConvertAll(item => new Order.OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                })
            };

            order.CalculateTotal();

            foreach (var item in request.OrderItems)
            {
                var stockCheck = await _stockService.CheckStock(item.ProductId);

                if (!stockCheck)
                {
                    throw new DomainException($"There is no stcok for this item {item.ProductId}");
                }
            }

            await _orderRepository.AddAsync(order);
            var dbresult =  await _orderRepository.SaveChangesAsync();

            if (!dbresult)
            {
               throw new DomainException($"Create Order Failed! {request.OrderNumber}");
            }

            var orderCreatedEvent = new OrderCreatedEvent
            {
                OrderId = order.Id,
                OrderNumber = order.OrderNumber,
                CustomerEmail = order.CustomerEmail,
                TotalAmount = order.TotalPrice,
                OrderItems = request.OrderItems.ConvertAll(item => new OrderCreatedEvent.OrderCreatedItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                })
            };

            await _publisher.Publish(orderCreatedEvent);

            return order;

        }
    }
}
