using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.ViewModels.Setting;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlkhabeerAccountant.Views.Setting
{
    /// <summary>
    /// Interaction logic for PaymentMethodsView.xaml
    /// </summary>
    public partial class PaymentMethodsSettingView : UserControl
    {
        public PaymentMethodsSettingView()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<PaymentMethodSettingViewModel>();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGridRowNumberHelper.HandleLoadingRow(sender, e);
        }
    }
}
