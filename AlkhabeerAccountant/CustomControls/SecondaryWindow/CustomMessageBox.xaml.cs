using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AlkhabeerAccountant.CustomControls.SecondaryWindow
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public bool Result { get; private set; }

        public CustomMessageBox(string message, string title = "رسالة النظام",
                                string icon = "info", bool showCancel = false)
        {
            InitializeComponent();

            TitleText.Text = title;
            MessageText.Text = message;
            CancelButton.Visibility = showCancel ? Visibility.Visible : Visibility.Collapsed;

            // Load icon dynamically
            string iconPath = icon switch
            {
                "success" => "pack://application:,,,/AlkhabeerAccountant;component/Resources/right.png",
                "error" => "pack://application:,,,/AlkhabeerAccountant;component/Resources/error.png",
                "warning" => "pack://application:,,,/AlkhabeerAccountant;component/Resources/warning.png",
                _ => "pack://application:,,,/AlkhabeerAccountant;component/Resources/info.png",
            };

            if (File.Exists(iconPath) == false)
            {
                try
                {
                    IconImage.Source = new BitmapImage(new Uri(iconPath, UriKind.Absolute));
                }
                catch { /* ignore icon error */ }
            }
            else
            {
                IconImage.Source = new BitmapImage(new Uri(iconPath, UriKind.Absolute));
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
        }

        public static bool ShowDelete(string message = "هل تريد الحذف", string title = "تنبيه")
        {
            var msg = new CustomMessageBox(message, title, "info", true)
            {
                Owner = Application.Current.MainWindow
            };
            msg.ShowDialog();
            return msg.Result;
        }

        public static bool Show(string message, string title = "تنبيه",
                        string icon = "info", bool showCancel = false)
        {
            var msg = new CustomMessageBox(message, title, icon, showCancel)
            {
                Owner = Application.Current.MainWindow
            };
            msg.ShowDialog();
            return msg.Result;
        }
    }
}
