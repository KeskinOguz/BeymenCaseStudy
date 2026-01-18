using CommonModule.Data;
using Microsoft.EntityFrameworkCore;
using OrderModule.Domain;
using OrderModule.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order, OrderDbContext>, IOrderRepository
    {
        public OrderRepository(OrderDbContext context) : base(context)
        {
        }
        public async Task<Order?> GeOrderWithOrderItemsByOrderNumber(string orderNumber)
        {
            return await _dbSet.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }
    }
}
