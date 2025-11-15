using AlkhabeerAccountant.Shared;
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
using System.Windows.Shapes;

namespace AlkhabeerAccountant.Views.Setting
{
    /// <summary>
    /// Interaction logic for PermissionsDialogWindow.xaml
    /// </summary>
    public partial class PermissionsDialogWindow : BaseWindow
    {
        public PermissionsDialogWindow()
        {
            InitializeComponent();
            //DataContext = Ioc.Default.GetService<UserSettingViewModel>();
            ApplyCenteredSizing();
        }
        protected override void ApplyCenteredSizing()
        {
            var workArea = SystemParameters.WorkArea;
            double widthRatio = 0.9;
            double heightRatio = 0.9;
            double verticalShiftRatio = 0.011;

            Width = workArea.Width * widthRatio;
            Height = workArea.Height * heightRatio;

            Left = workArea.Left + (workArea.Width - Width) / 2;
            Top = workArea.Top + (workArea.Height - Height) / 2 + (workArea.Height * verticalShiftRatio);
        }
    }
}
