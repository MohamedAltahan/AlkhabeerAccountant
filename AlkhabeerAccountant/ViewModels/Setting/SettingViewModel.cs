using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.ViewModel;
using AlkhabeerAccountant.Views.Setting;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;



namespace AlkhabeerAccountant.ViewModels.Setting
{
    public class SettingViewModel : INotifyPropertyChanged
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

        public SettingViewModel()
        {
            NavigateCommand = new RelayCommand(NavigateToPage);

            // Set default page to Company
            ActivePage = "Company";
            CurrentContent = new CompanyView();
        }

        private void NavigateToPage(object? parameter)
        {
            if (parameter is string pageType)
            {
                ActivePage = pageType;

                switch (pageType)
                {
                    case "Company":
                        CurrentContent = new CompanyView();
                        break;
                    //case "Cost":
                    //    CurrentContent = new CostView();
                    //    break;
                    //case "Banks":
                    //    CurrentContent = new BanksView();
                    //    break;
                    //case "Stores":
                    //    CurrentContent = new StoresView();
                    //    break;
                    case "Users":
                        CurrentContent = new UsersView();
                        break;
                        //case "Tax":
                        //    CurrentContent = new TaxView();
                        //    break;
                        //case "Discounts":
                        //    CurrentContent = new DiscountsView();
                        //    break;
                        //case "PaymentMethods":
                        //    CurrentContent = new PaymentMethodsView();
                        //    break;
                        //case "Currency":
                        //    CurrentContent = new CurrencyView();
                        //    break;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}