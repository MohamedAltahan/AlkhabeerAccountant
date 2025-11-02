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

    // ✅ Use service layer instead of repository
    protected BaseService<T> _service;

    public int TotalCount => Items?.Count ?? 0;
    public string PageInfo => $"صفحة {CurrentPage} من {TotalPages}";

    // ✅ By default, use service.GetPagedAsync
    protected virtual async Task<PaginatedResult<T>> GetPagedDataAsync(int page, int size)
    {
        return await _service.GetPagedAsync(page, size);
    }

    protected BasePagedViewModel(BaseService<T> service)
    {
        _service = service;
    }

    // ================= Pagination =================
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

    // ================= Generic CRUD Support =================
    [ObservableProperty]
    private T? selectedItem;

    protected virtual async Task AddAsync(T entity)
    {
        var result = await _service.AddAsync(entity);
        if (result.IsSuccess)
            ToastService.Success("تمت الإضافة بنجاح");
        else
            ToastService.Warning(result.ErrorMessage);
    }

    protected virtual async Task UpdateAsync(T entity)
    {
        var result = await _service.UpdateAsync(entity);
        if (result.IsSuccess)
            ToastService.Updated("تم التحديث بنجاح");
        else
            ToastService.Warning(result.ErrorMessage);
    }


    public virtual async Task DeleteAsync(int entity)
    {
        var result = await _service.DeleteAsync(entity);
        if (result.IsSuccess)
            ToastService.Deleted("تم الحذف بنجاح");
        else
            ToastService.Warning(result.ErrorMessage);
    }
}
