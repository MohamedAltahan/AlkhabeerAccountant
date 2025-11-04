using Alkhabeer.Core.Shared;
using Alkhabeer.Service.Base;
using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.Services;
using AlkhabeerAccountant.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public abstract partial class BasePagedViewModel<T> : BaseViewModel, IBasePagedViewModel
    where T : class
{
    [ObservableProperty] private int currentPage = 1;
    [ObservableProperty] private int pageSize = 40;
    [ObservableProperty] private int totalPages;
    [ObservableProperty] private ObservableCollection<T> items = new();

    //========automate bind selected item form the table to inputs
    [ObservableProperty] private T? selectedItem;
    //private T _selectedItem;
    //public T SelectedItem
    //{
    //    get => _selectedItem;
    //    set
    //    {
    //        if (!EqualityComparer<T>.Default.Equals(_selectedItem, value))
    //        {
    //            SetProperty(ref _selectedItem, value);
    //            OnSelectedItemChanged(value);
    //        }
    //    }
    //}

    partial void OnSelectedItemChanged(T value)
    {
        if (value == null) return;

        // Copy any matching property names (case-insensitive)
        var entityProps = typeof(T).GetProperties();
        var vmProps = GetType().GetProperties();

        foreach (var prop in entityProps)
        {
            var target = vmProps.FirstOrDefault(p =>
                string.Equals(p.Name, prop.Name, StringComparison.OrdinalIgnoreCase) &&
                p.CanWrite && p.PropertyType == prop.PropertyType);

            if (target != null)
            {
                var val = prop.GetValue(value);
                target.SetValue(this, val);
            }
        }
    }

    // Use service layer instead of repository
    protected BaseService<T> _service;
    public int TotalCount => Items?.Count ?? 0;
    public string PageInfo => $"صفحة {CurrentPage} من {TotalPages}";

    protected BasePagedViewModel(BaseService<T> service)
    {
        _service = service;
    }


    // ================= Pagination section=================
    [RelayCommand]
    public async Task LoadPageAsync()
    {
        var result = await GetPagedDataAsync(CurrentPage, PageSize);

        Items = new ObservableCollection<T>(result.Data);
        TotalPages = result.TotalPages;
        OnPropertyChanged(nameof(PageInfo));
    }

    protected virtual async Task<PaginatedResult<T>> GetPagedDataAsync(int pageNumber, int pageSize)
    {
        return await _service.GetPagedAsync(pageNumber, pageSize);
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

    // ================= Generic T CRUD section =================

    protected virtual async Task AddAsync(T entity)
    {
        var result = await _service.AddAsync(entity);

        if (result.IsSuccess)
            ToastService.Success();
        else
            ToastService.Warning(result.ErrorMessage);
    }

    protected virtual async Task UpdateAsync(T entity)
    {
        var result = await _service.UpdateAsync(entity);

        if (result.IsSuccess)
            ToastService.Updated();
        else
            ToastService.Error(result.ErrorMessage);
    }

    public virtual async Task DeleteAsync(int entity)
    {
        var result = await _service.DeleteAsync(entity);

        if (result.IsSuccess)
            ToastService.Deleted();
        else
            ToastService.Error();
    }

}
