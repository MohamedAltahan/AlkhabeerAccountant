using Alkhabeer.Core.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlkhabeerAccountant.ViewModels.Setting
{

    public abstract partial class BasePagedViewModel<T> : BaseViewModel
    {
        // ✅ Shared properties (inherited by all)
        [ObservableProperty] private int currentPage = 1;
        [ObservableProperty] private int pageSize = 20;
        [ObservableProperty] private int totalPages;
        [ObservableProperty] private ObservableCollection<T> items = new();

        public string PageInfo => $"صفحة {CurrentPage} من {TotalPages}";

        // ✅ Abstract method that derived VMs must implement
        protected abstract Task<PaginatedResult<T>> GetPagedDataAsync(int page, int size);

        [RelayCommand]
        public async Task LoadPageAsync()
        {
            var result = await GetPagedDataAsync(CurrentPage, PageSize);
            Items = new ObservableCollection<T>(result.Data);
            TotalPages = result.TotalPages;
            OnPropertyChanged(nameof(PageInfo));
        }

        [RelayCommand]
        public async Task NextPageAsync()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                await LoadPageAsync();
            }
        }

        [RelayCommand]
        public async Task PreviousPageAsync()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadPageAsync();
            }
        }

    }
}
