using CommonModule.Data;
using NotificationModule.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationModule.Infrastructure.Repositories
{
    public interface INotificationRepository : IGenericRepository<NotificationLog>
    {

    }
}
