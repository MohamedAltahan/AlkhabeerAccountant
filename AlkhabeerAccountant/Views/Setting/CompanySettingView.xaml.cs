using Alkhabeer.Data.Repositories;
using AlkhabeerAccountant.ViewModels.Setting;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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

    public partial class CompanySettingView : UserControl
    {
        public CompanySettingView()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<CompanySettingViewModel>();
        }
    }
}
