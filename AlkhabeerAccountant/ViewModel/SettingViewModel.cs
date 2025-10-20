using AlkhabeerAccountant.Helpers;
using System.Windows;
using System.Windows.Input;

namespace AlkhabeerAccountant.ViewModel
{
    public class SettingViewModel
    {
        public ICommand ShowMessageCommand { get; }

        public SettingViewModel()
        {
            ShowMessageCommand = new RelayCommand(ExecuteShowMessage, CanExecute);
        }

        private void ExecuteShowMessage(object? parameter)
        {
            MessageBox.Show("تم تنفيذ الأمر بنجاح 🎉", "Alkhabeer");
        }

        private bool CanExecute(object? parameter)
        {
            return true;
        }
    }
}

