using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AlkhabeerAccountant.Helpers
{
    public static class DataGridRowNumberHelper
    {
        //get row number for any table
        public static void HandleLoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (sender is not DataGrid grid) return;
            if (grid.DataContext is not IBasePagedViewModel viewModel) return;

            int rowNumber = ((viewModel.CurrentPage - 1) * viewModel.PageSize) + e.Row.GetIndex() + 1;
            e.Row.Header = rowNumber.ToString();
        }
    }

    public interface IBasePagedViewModel
    {
        int CurrentPage { get; }
        int PageSize { get; }
    }
}
