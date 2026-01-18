using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NotificationModule.Domain;
using NotificationModule.Infrastructure.Data;
using NotificationModule.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationModule.Application.GetNotifications
{
    internal class GetNotificationQueryHandler : IRequestHandler<GetNotificationQuery, IList<NotificationLog>>
    {
        private readonly INotificationRepository _notificationRepository;
        public GetNotificationQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<IList<NotificationLog>> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
        {

            var notifications = await _notificationRepository.GetAllAsync();

            return notifications.ToList();
        }
    }
}
