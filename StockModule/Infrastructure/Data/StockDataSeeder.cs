using CommonModule.Data;
using Microsoft.EntityFrameworkCore;
using StockModule.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockModule.Infrastructure.Data
{
    public class StockDataSeeder : IModuleSeeder
    {
        private readonly StockDbContext _context;
        public StockDataSeeder(StockDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            if (!await _context.Stocks.AnyAsync())
            {

                var defaultStocks = new List<Stock>
                {
                    new() 
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 10,
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 5,
                    },
                    new() 
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 15,
                    }
                };

                _context.Stocks.AddRange(defaultStocks);


                await _context.SaveChangesAsync();
            }
        }
    }
}
