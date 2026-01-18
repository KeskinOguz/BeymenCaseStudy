using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModule.Services
{
    public interface IStockService
    {
        public Task<bool> CheckStock(Guid productId);
    }
}
