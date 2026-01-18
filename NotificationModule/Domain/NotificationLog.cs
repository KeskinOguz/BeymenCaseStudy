using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationModule.Domain
{
    public class NotificationLog
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public NotificationType NotificationType { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }

    public enum NotificationType
    {
        Email,
        SMS
    }
}
