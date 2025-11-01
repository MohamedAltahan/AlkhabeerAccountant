﻿using AlkhabeerAccountant.Helpers;
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
    /// Interaction logic for BanksView.xaml
    /// </summary>
    public partial class BanksSettingView : UserControl
    {
        public BanksSettingView()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<BankSettingViewModel>();
        }

        //private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        //{
        //    // Calculate the row number based on current page
        //    var viewModel = DataContext as BankSettingViewModel;
        //    if (viewModel != null)
        //    {
        //        int rowNumber = ((viewModel.CurrentPage - 1) * viewModel.PageSize) + e.Row.GetIndex() + 1;
        //        e.Row.Header = rowNumber.ToString();
        //    }
        //}
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGridRowNumberHelper.HandleLoadingRow(sender, e);
        }
    }


}
