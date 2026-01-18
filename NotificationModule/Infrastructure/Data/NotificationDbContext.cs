using Microsoft.EntityFrameworkCore;
using NotificationModule.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationModule.Infrastructure.Data
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

        public DbSet<NotificationLog> NotificationLogs => Set<NotificationLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<NotificationLog>(entity =>
            {
                entity.ToTable("NotificationLogs");
                entity.HasKey(e => e.Id);
            });
        }
    }
}
