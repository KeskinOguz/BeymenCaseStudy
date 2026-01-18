using CommonModule.Data;
using NotificationModule.Domain;
using NotificationModule.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NotificationModule.Infrastructure.Repositories
{
    public class NotificationRepository : GenericRepository<NotificationLog, NotificationDbContext>, INotificationRepository
    {
        public NotificationRepository(NotificationDbContext context) : base(context)
        {
        }
    }
}
