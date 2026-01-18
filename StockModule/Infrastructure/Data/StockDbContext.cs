using Microsoft.EntityFrameworkCore;
using StockModule.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockModule.Infrastructure.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }
        public DbSet<Stock> Stocks => Set<Stock>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stocks");
                entity.HasKey(e => e.Id);
            });
        }
    }
}
