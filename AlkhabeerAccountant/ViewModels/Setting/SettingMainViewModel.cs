using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.Views.Setting;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class SettingMainViewModel : BaseViewModel<object>
    {
        [ObservableProperty]
        private object currentContent;
        [ObservableProperty]
        private string activePage;


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
                //"Users" => new UsersSettingView(),
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
