using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.Views.Setting;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public class SettingMainViewModel : INotifyPropertyChanged
    {
        private object _currentContent;
        private string _activePage;

        public object CurrentContent
        {
            get => _currentContent;
            set
            {
                _currentContent = value;
                OnPropertyChanged();
            }
        }

        public string ActivePage
        {
            get => _activePage;
            set
            {
                _activePage = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateCommand { get; }

        public SettingMainViewModel()
        {
            NavigateCommand = new RelayCommand(NavigateToPage);

            // Default page
            NavigateToPage("Company");
        }

        private void NavigateToPage(object? parameter)
        {
            if (parameter is not string pageType) return;

            ActivePage = pageType;

            CurrentContent = pageType switch
            {
                "Company" => new CompanySettingView(),
                "InventoryCost" => new InventorySettingView(),
                "Banks" => new BanksSettingView(),
                "Treasures" => new TreasurySettingView(),
                "Users" => new UsersSettingView(),
                "Roles" => new RolesSettingView(),
                "PaymentMethods" => new PaymentMethodsSettingView(),
                "Currency" => new CurrencySettingView(),
                _ => new CompanySettingView() // fallback
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
