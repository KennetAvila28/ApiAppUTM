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