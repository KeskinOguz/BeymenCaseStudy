using CommonModule.Data;
using StockModule.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockModule.Infrastructure.Repositories
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        Task<Stock?> GetByProductIdAsync(Guid productId);
    }
}
