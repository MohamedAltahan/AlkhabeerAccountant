using Notifications.Wpf;

namespace AlkhabeerAccountant.Services
{
    public static class ToastService
    {
        private static readonly NotificationManager _manager = new();

        public static void Success(string? message = "تم الحفظ بنجاح", string title = "تمت العملية")
        {
            _manager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Success
            });
        }
        public static void Updated(string message = "تم التحديث بنجاح", string title = "تمت العملية")
        {
            _manager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Success
            });
        }
        public static void Added(string message = "تمت الإضافة بنجاح", string title = "تمت العملية")
        {
            _manager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Success
            });
        }
        public static void Deleted(string message = "تم الحذف بنجاح", string title = "تمت العملية")
        {
            _manager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Success
            });
        }
        public static void Error(string? message = "يوجد خطاء ما", string? title = "خطأ")
        {
            _manager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Error
            });
        }

        public static void Warning(string? message = "تحذير", string title = "تحذير")
        {
            _manager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Warning
            });
        }

        public static void Info(string message, string title = "معلومة")
        {
            _manager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Information
            });
        }
    }
}
