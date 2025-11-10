using Alkhabeer.Core.Shared;
using Alkhabeer.Service.Base;
using AlkhabeerAccountant.CustomControls.SecondaryWindow;
using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.Services;
using AlkhabeerAccountant.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

public abstract partial class BasePagedViewModel<T> : BaseViewModel<T>, IBasePagedViewModel where T : class
{
    [ObservableProperty] private int currentPage = 1;
    [ObservableProperty] private int pageSize = 2;
    [ObservableProperty] private int totalPages;
    [ObservableProperty] private ObservableCollection<T> items = new();
    [ObservableProperty] private T? selectedItem;
    [ObservableProperty] private int totalCount;
    public string PageInfo => $"صفحة {CurrentPage} من {TotalPages}";

    protected BasePagedViewModel(BaseService<T> service) : base(service) { }

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

    // ================= Pagination section=================
    public async Task LoadPageAsync()
    {
        var result = await GetPagedDataAsync(CurrentPage, PageSize);

        Items = new ObservableCollection<T>(result.Data);
        TotalPages = result.TotalPages;
        TotalCount = result.TotalCount;
        OnPropertyChanged(nameof(PageInfo));
    }

    protected virtual async Task<PaginatedResult<T>> GetPagedDataAsync(int pageNumber, int pageSize)
    {
        return await _service.GetPagedAsync(pageNumber, pageSize);
    }

    protected abstract T MapEntityFromView();

    protected virtual async Task SaveOrUpdateAsync()
    {
        if (!ValidateForm()) return;

        var entity = MapEntityFromView();
        var result = await _service.SaveOrUpdateAsync(entity);

        if (result.IsSuccess)
        {
            ToastService.Added();
            CurrentPage = 1;
            FormResetHelper.Reset(this);
            await LoadPageAsync();
        }
        else
        {
            ToastService.Warning(result.ErrorMessage);
        }
    }

    public virtual async Task DeleteAsync()
    {
        if (SelectedItem == null)
            return;

        if (!CustomMessageBox.ShowDelete())
            return;

        // Use reflection or BaseEntity pattern to get Id
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty == null)
        {
            ToastService.Error("لا يمكن حذف هذا السجل لأنه لا يحتوي على خاصية Id");
            return;
        }

        var idValue = idProperty.GetValue(SelectedItem);
        if (idValue is not int id)
        {
            ToastService.Error("فشل حذف السجل: رقم المعرف غير صالح");
            return;
        }

        var result = await _service.DeleteAsync(id);

        if (result.IsSuccess)
        {
            await LoadPageAsync();
            ToastService.Deleted();
            FormResetHelper.Reset(this);
        }
        else
        {
            ToastService.Error(result.ErrorMessage);
        }
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
