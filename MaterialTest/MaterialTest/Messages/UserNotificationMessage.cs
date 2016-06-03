using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAD.Plugin.MessagingService.Core;

namespace MaterialTest
{
    public class UserNotificationMessage : IMessage
    {
        public readonly UserNotification UserNotification;

        public UserNotificationMessage(UserNotification userNotification)
        {
            UserNotification = userNotification;
        }
    }
}
