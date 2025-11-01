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
        /// <summary>
        /// Sets the row header with correct serial number based on pagination.
        /// Works for any DataGrid whose DataContext inherits from BasePagedViewModel.
        /// </summary>
        public static void HandleLoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (sender is not DataGrid grid) return;
            if (grid.DataContext is not IBasePagedViewModel viewModel) return;

            int rowNumber = ((viewModel.CurrentPage - 1) * viewModel.PageSize) + e.Row.GetIndex() + 1;
            e.Row.Header = rowNumber.ToString();
        }
    }

    /// <summary>
    /// Small interface for pagination props so helper doesn’t need to depend on generic BasePagedViewModel&lt;T&gt;.
    /// </summary>
    public interface IBasePagedViewModel
    {
        int CurrentPage { get; }
        int PageSize { get; }
    }
}
