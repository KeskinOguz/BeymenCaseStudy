using CommonModule.Services;
using StockModule.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockModule.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<bool> CheckStock(Guid productId)
        {
            var stock = await _stockRepository.GetByProductIdAsync(productId);

            if (stock == null)
            {
                return false;
            }

            return stock.Quantity > 0;
        }
    }
}
