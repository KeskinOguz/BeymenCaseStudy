using Microsoft.EntityFrameworkCore;
using OrderModule.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModule.Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Order.OrderItem> OrderItems => Set<Order.OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders");
                builder.HasKey(o => o.Id);

                builder.HasMany(o => o.OrderItems)
                       .WithOne(i => i.Order)
                       .HasForeignKey(i => i.OrderId)
                       .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<Order.OrderItem>(builder =>
            {
                builder.ToTable("OrderItems");
                builder.HasKey(i => i.Id);
                builder.Property(i => i.UnitPrice).HasPrecision(18, 2);
            });
        }
    }
}
