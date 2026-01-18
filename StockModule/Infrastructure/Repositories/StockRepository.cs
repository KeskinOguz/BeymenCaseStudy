using CommonModule.Data;
using Microsoft.EntityFrameworkCore;
using StockModule.Domain;
using StockModule.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockModule.Infrastructure.Repositories
{
    public class StockRepository : GenericRepository<Stock, StockDbContext>, IStockRepository
    {
        public StockRepository(StockDbContext context) : base(context)
        {
        }

        public async Task<Stock?> GetByProductIdAsync(Guid productId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

    }
}
