using CommonModule.Data;
using OrderModule.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Infrastructure.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GeOrderWithOrderItemsByOrderNumber(string orderNumber);
    }
}
