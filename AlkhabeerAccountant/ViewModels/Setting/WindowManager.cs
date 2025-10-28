using System.Linq;
using System.Windows;

namespace AlkhabeerAccountant.Helpers
{
    public static class WindowManager
    {
        public static void ShowSingle<T>() where T : Window, new()
        {
            var existingWindow = Application.Current.Windows.OfType<T>().FirstOrDefault();
            if (existingWindow != null)
            {
                if (existingWindow.WindowState == WindowState.Minimized)
                    existingWindow.WindowState = WindowState.Normal;

                existingWindow.Activate();
                return;
            }

            var newWindow = new T();
            newWindow.Show();
        }
    }
}
