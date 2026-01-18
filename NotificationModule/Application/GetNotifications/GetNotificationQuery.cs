using MediatR;
using NotificationModule.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationModule.Application.GetNotifications
{
    public class GetNotificationQuery : IRequest<IList<NotificationLog>>
    {

    }
}
