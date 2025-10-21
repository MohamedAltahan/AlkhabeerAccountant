using AlkhabeerAccountant.Helpers;
using System.Windows;
using System.Windows.Input;
using AlkhabeerAccountant.Views;
using AlkhabeerAccountant.Views.Setting;

namespace AlkhabeerAccountant.ViewModel
{
    public class MainViewModel
    {
        public ICommand OpenSettingsCommand { get; }

        public MainViewModel()
        {
            OpenSettingsCommand = new RelayCommand(OpenSettings);
        }

        private void OpenSettings(object? parameter)
        {
            var mainWindow = Application.Current.MainWindow;
            if (mainWindow == null) return;

            var settingsWindow = new Setting
            {
                Owner = mainWindow,
                ShowInTaskbar = false,
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            // ✅ Get the screen's working area (excluding taskbar)
            var workArea = SystemParameters.WorkArea;

            double widthRatio = 0.9;
            double heightRatio = 0.9;

            // ✅ Apply ratio-based size
            settingsWindow.Width = workArea.Width * widthRatio;
            settingsWindow.Height = workArea.Height * heightRatio;

            double verticalShiftRatio = 0.05; // = 10% down
            // ✅ Center it on screen
            settingsWindow.Left = workArea.Left + (workArea.Width - settingsWindow.Width) / 2;
            settingsWindow.Top = workArea.Top + (workArea.Height - settingsWindow.Height) / 2 + (workArea.Height * verticalShiftRatio);

            settingsWindow.Show();
        }

    }
}
