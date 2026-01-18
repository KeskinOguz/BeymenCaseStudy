using CommonModule;
using CommonModule.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockModule.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockModule.Application
{
    public class OrderCreatedStockHandler : RabbitMQConsumer<OrderCreatedEvent>
    {
        public OrderCreatedStockHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected async override Task HandleMessageAsync(OrderCreatedEvent @event, IServiceProvider serviceProvider)
        {
            var _stockRepository = serviceProvider.GetRequiredService<IStockRepository>();

            foreach (var item in @event.OrderItems)
            {
       
                var stock = await _stockRepository.GetByProductIdAsync(item.ProductId);

                if (stock == null)
                {
                    throw new Exception($"There is no stock record for this ProductId: {item.ProductId}");
                }

                stock.Quantity -= item.Quantity;
                await _stockRepository.UpdateAsync(stock);
            }

            await _stockRepository.SaveChangesAsync();

            await Task.CompletedTask;
        }
    }
}
