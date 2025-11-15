using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Core.Validation;
using Alkhabeer.Service;
using AlkhabeerAccountant.CustomControls.SecondaryWindow;
using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class BankSettingViewModel : BasePagedViewModel<Bank>
    {
        public BankSettingViewModel(BankService bankService) : base(bankService)
        {
            _ = LoadPageAsync();
        }

        [ObservableProperty, RequiredEx, MaxLengthEx(50)]
        private string bankName = null!;

        [ObservableProperty, RequiredEx, MaxLengthEx(50)]
        private string accountName = null!;

        [ObservableProperty, RequiredEx, MaxLengthEx(25), NumbersOnlyEx]
        private string accountNumber = null!;

        [ObservableProperty, MaxLengthEx(25), NumbersOnlyEx]
        private string? iban;

        [ObservableProperty, MaxLengthEx(300)]
        private string? notes;

        [ObservableProperty]
        private bool isActive = true;

        // ====================== Save/Update ======================
        [RelayCommand]
        private async Task SaveOrUpdateAsync()
        {
            if (!ValidateForm()) return;

            var entity = SelectedItem ?? new Bank();

            entity.BankName = BankName;
            entity.AccountName = AccountName;
            entity.AccountNumber = AccountNumber;
            entity.Iban = Iban;
            entity.Notes = Notes;
            entity.IsActive = IsActive;

            await CheckSaveResultAsync(await _service.SaveOrUpdateAsync(entity));
        }

        // ====================== Delete ======================
        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (SelectedItem == null) return;
            if (!CustomMessageBox.ShowDelete()) return;

            await CheckDeleteResultAsync(await _service.DeleteAsync(SelectedItem.Id));
        }
    }
}
