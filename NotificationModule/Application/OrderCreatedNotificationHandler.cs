using CommonModule;
using CommonModule.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotificationModule.Domain;
using NotificationModule.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationModule.Application
{
    public class OrderCreatedNotificationHandler(IServiceProvider serviceProvider, ILogger<OrderCreatedNotificationHandler> logger) : RabbitMQConsumer<OrderCreatedEvent>(serviceProvider)
    {
        private readonly ILogger<OrderCreatedNotificationHandler> _logger = logger;

        protected override async Task HandleMessageAsync(OrderCreatedEvent @event, IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<NotificationDbContext>();

            bool emailSent = true;  //await FakeEmailService.SendAsync(@event.UserEmail, "Order Confirmed!");

            _logger.LogInformation("Processing notification for Order: {OrderId}", @event.OrderId);

            var log = new NotificationLog
            {
                Id = Guid.NewGuid(),
                OrderId = @event.OrderId,
                CustomerEmail = @event.CustomerEmail,
                ErrorMessage = emailSent ? null : "Failed to send email",
                NotificationType = NotificationType.Email,
                IsSuccess = emailSent,
                SentAt = DateTime.UtcNow
            };

            await dbContext.NotificationLogs.AddAsync(log);
            await dbContext.SaveChangesAsync();

            _logger.LogInformation("Notification log saved to PostgreSQL.");

            await Task.CompletedTask;
        }
    }
}
