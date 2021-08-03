using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Radzen;

namespace AppUTM.Extensions
{
    internal static class NotificationExtensions
    {
        public static void ShowNotification(NotificationService service, NotificationMessage message)
        {
            service.Notify(message);
        }
    }
}