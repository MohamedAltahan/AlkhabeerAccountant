using Alkhabeer.Core.Shared;
using Alkhabeer.Core.Shared.Enums;
using Alkhabeer.Service.Base;
using AlkhabeerAccountant.CustomControls.SecondaryWindow;
using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.Services;
using AlkhabeerAccountant.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

public abstract partial class BasePagedViewModel<T> : BaseViewModel<T>, IBasePagedViewModel where T : class
{
    // ==================== Pagination ====================
    [ObservableProperty] private int currentPage = 1;
    [ObservableProperty] private int pageSize = 2;
    [ObservableProperty] private int totalPages;
    [ObservableProperty] private ObservableCollection<T> items = new();
    [ObservableProperty] private T? selectedItem;
    [ObservableProperty] private int totalCount;

    public string PageInfo => $"صفحة {CurrentPage} من {TotalPages}";

    protected BasePagedViewModel(BaseService<T> service) : base(service)
    {
        FormMode = FormMode.View;
    }
    [ObservableProperty] private FormMode formMode;
    // ==================== Form State ====================
    public bool IsFormEnabled => FormMode != FormMode.View;
    public bool IsSaveEnabled => FormMode != FormMode.View;
    public bool IsEditEnabled => FormMode == FormMode.View && SelectedItem != null;
    public bool IsDeleteEnabled => FormMode == FormMode.View && SelectedItem != null;
    partial void OnFormModeChanged(FormMode value)
    {
        OnPropertyChanged(nameof(IsFormEnabled));
        OnPropertyChanged(nameof(IsSaveEnabled));
        OnPropertyChanged(nameof(IsEditEnabled));
        OnPropertyChanged(nameof(IsDeleteEnabled));
    }

    // =============== Binding Auto Mapping ==================
    partial void OnSelectedItemChanged(T value)
    {
        if (value == null)
        {
            FormMode = FormMode.View;
            OnPropertyChanged(nameof(IsEditEnabled));
            OnPropertyChanged(nameof(IsDeleteEnabled));
            return;
        }

        var entityProps = typeof(T).GetProperties();
        var vmProps = GetType().GetProperties();

        foreach (var prop in entityProps)
        {
            var vmProp = vmProps.FirstOrDefault(p =>
                p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase) &&
                p.CanWrite &&
                p.PropertyType == prop.PropertyType);

            if (vmProp == null) continue;

            if (Attribute.IsDefined(vmProp, typeof(AlkhabeerAccountant.Helpers.Attributes.IgnoreMappingAttribute)))
                continue;

            vmProp.SetValue(this, prop.GetValue(value));
        }

        FormMode = FormMode.View;
        //  update UI
        OnPropertyChanged(nameof(IsEditEnabled));
        OnPropertyChanged(nameof(IsDeleteEnabled));
    }

    // ==================== Commands ======================

    [RelayCommand]
    public virtual void Add()
    {
        SelectedItem = null;
        FormResetHelper.Reset(this);
        FormMode = FormMode.Add;
    }

    [RelayCommand]
    public virtual void Edit()
    {
        if (SelectedItem == null) return;
        FormMode = FormMode.Edit;
    }

    // ==================== Pagination Load ====================
    protected virtual async Task LoadPageAsync()
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

    // ==================== Save / Delete Results ====================
    public async Task CheckSaveResultAsync(Result result)
    {
        if (result.IsSuccess)
        {
            ToastService.Added();
            CurrentPage = 1;
            FormResetHelper.Reset(this);

            FormMode = FormMode.View;

            await LoadPageAsync();
        }
        else
        {
            ToastService.Warning(result.ErrorMessage);
        }
    }

    public async Task CheckDeleteResultAsync(Result result)
    {
        if (result.IsSuccess)
        {
            ToastService.Deleted();
            FormResetHelper.Reset(this);
            FormMode = FormMode.View;
            await LoadPageAsync();
        }
        else
        {
            ToastService.Error(result.ErrorMessage);
        }
    }
}
