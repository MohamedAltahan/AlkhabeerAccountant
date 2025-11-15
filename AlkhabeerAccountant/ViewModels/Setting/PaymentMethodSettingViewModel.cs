using Alkhabeer.Core.Models;
using Alkhabeer.Core.Validation;
using Alkhabeer.Services;
using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.CustomControls.SecondaryWindow;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class PaymentMethodSettingViewModel : BasePagedViewModel<PaymentMethod>
    {
        private new readonly PaymentMethodService _service;

        public PaymentMethodSettingViewModel(PaymentMethodService service)
            : base(service)
        {
            _service = service;
            _ = LoadPageAsync();
        }

        [ObservableProperty, RequiredEx]
        private string name = string.Empty;

        [ObservableProperty]
        private string? description;

        [ObservableProperty]
        private bool isActive = true;


        [RelayCommand]
        private async Task SaveOrUpdateAsync()
        {
            if (!ValidateForm()) return;

            var entity = SelectedItem ?? new PaymentMethod();

            entity.Name = Name;
            entity.Description = Description;
            entity.IsActive = IsActive;

            var result = await _service.SaveOrUpdateAsync(entity);
            await CheckSaveResultAsync(result);
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (SelectedItem == null) return;
            if (!CustomMessageBox.ShowDelete()) return;

            var result = await _service.DeleteAsync(SelectedItem.Id);
            await CheckDeleteResultAsync(result);
        }
    }
}
