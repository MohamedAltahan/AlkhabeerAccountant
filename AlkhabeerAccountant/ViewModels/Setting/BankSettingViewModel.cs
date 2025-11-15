using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Core.Validation;
using Alkhabeer.Service;
using AlkhabeerAccountant.CustomControls.SecondaryWindow;
using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class BankSettingViewModel : BasePagedViewModel<Bank>
    {
        public BankSettingViewModel(BankService bankService) : base(bankService)
        {
            IsFormEnabled = false;
            IsSaveEnabled = false;
            IsDeleteEnabled = false;
            IsEditEnabled = false;
            _ = LoadPageAsync();
        }

        // ================ UI State Properties ================
        [ObservableProperty]
        private bool isFormEnabled;

        [ObservableProperty]
        private bool isSaveEnabled;

        [ObservableProperty]
        private bool isEditEnabled;

        [ObservableProperty]
        private bool isDeleteEnabled;

        // ================ Data Properties ================
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

        // ===================== Commands ======================

        [RelayCommand]
        private void Add()
        {
            SelectedItem = null;

            BankName = "";
            AccountName = "";
            AccountNumber = "";
            Iban = "";
            Notes = "";
            IsActive = true;

            IsFormEnabled = true;
            IsSaveEnabled = true;
            IsEditEnabled = false;
            IsDeleteEnabled = false;
        }

        public override void OnSelectedItemChanged(Bank value)
        {
            if (value == null)
            {
                IsEditEnabled = false;
                IsDeleteEnabled = false;
                return;
            }

            IsEditEnabled = true;
            IsDeleteEnabled = true;
            IsSaveEnabled = false;
            IsFormEnabled = false;
        }

        [RelayCommand]
        private void Edit()
        {
            if (SelectedItem == null) return;

            BankName = SelectedItem.BankName;
            AccountName = SelectedItem.AccountName;
            AccountNumber = SelectedItem.AccountNumber;
            Iban = SelectedItem.Iban;
            Notes = SelectedItem.Notes;
            IsActive = SelectedItem.IsActive;

            IsFormEnabled = true;
            IsSaveEnabled = true;
        }

        [RelayCommand]
        protected async Task SaveOrUpdateAsync()
        {
            if (!ValidateForm()) return;

            var entity = SelectedItem ?? new Bank();
            entity.BankName = BankName;
            entity.AccountName = AccountName;
            entity.AccountNumber = AccountNumber;
            entity.Iban = Iban;
            entity.Notes = Notes;
            entity.IsActive = IsActive;

            var result = await _service.SaveOrUpdateAsync(entity);
            await CheckSaveResultAsync(result);

            IsFormEnabled = false;
            IsSaveEnabled = false;
        }

        [RelayCommand]
        protected async Task DeleteAsync()
        {
            if (SelectedItem == null) return;
            if (!CustomMessageBox.ShowDelete()) return;

            var result = await _service.DeleteAsync(SelectedItem.Id);
            await CheckDeleteResultAsync(result);

            IsEditEnabled = false;
            IsDeleteEnabled = false;
        }
    }
}
