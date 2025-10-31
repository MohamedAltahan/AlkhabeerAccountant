using Alkhabeer.Core.Shared;
using Alkhabeer.Core.Models;
using Alkhabeer.Core.Validation;
using Alkhabeer.Data.Repositories;
using AlkhabeerAccountant.CustomControls.SecondaryWindow;
using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.Services;
using AlkhabeerAccountant.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class BankSettingViewModel : BasePagedViewModel<Bank>
    {
        private readonly BankRepository _bankRepository;
        public int PageOffset => (CurrentPage - 1) * PageSize;
        public BankSettingViewModel(BankRepository bankRepository)
        {
            _bankRepository = bankRepository;
            _ = LoadPageAsync(); // load first paged data
        }

        // ===================== Form Fields =====================
        [ObservableProperty]
        [RequiredEx]
        [MaxLengthEx(50)]
        private string bankName;

        [ObservableProperty]
        [RequiredEx]
        [MaxLengthEx(50)]
        private string accountName;

        [ObservableProperty]
        [RequiredEx]
        [MaxLengthEx(25)]
        [NumbersOnlyEx]
        private string accountNumber;

        [ObservableProperty]
        [MaxLengthEx(25)]
        [NumbersOnlyEx]
        private string? iban;

        [ObservableProperty]
        [MaxLengthEx(300)]
        private string? notes;

        [ObservableProperty]
        private bool isActive = true;

        // ===================== Table Data =====================
        [ObservableProperty]
        private Bank selectedBank;

        // ===================== Pagination Override =====================
        protected override async Task<PaginatedResult<Bank>> GetPagedDataAsync(int page, int size)
        {
            // You can replace this with a filtered version if needed:
            // return await _bankRepository.GetPagedFilteredAsync(SearchText, page, size);
            return await _bankRepository.GetPagedAsync(page, size);
        }

        // ===================== Commands =====================

        [RelayCommand]
        private async Task SaveAsync()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                ToastService.Warning(GetErrors(null)
                    .Cast<ValidationResult>()
                    .FirstOrDefault()?.ErrorMessage);
                return;
            }

            if (SelectedBank == null)
            {
                // Create new
                var newBank = new Bank
                {
                    BankName = BankName,
                    AccountName = AccountName,
                    AccountNumber = AccountNumber,
                    Iban = Iban,
                    Notes = Notes,
                    IsActive = IsActive
                };
                await _bankRepository.AddAsync(newBank);
                CurrentPage = 1;
                await LoadPageAsync(); // refresh table with pagination
                ToastService.Added();
            }
            else
            {
                // Update existing
                SelectedBank.BankName = BankName;
                SelectedBank.AccountName = AccountName;
                SelectedBank.AccountNumber = AccountNumber;
                SelectedBank.Iban = Iban;
                SelectedBank.Notes = Notes;
                SelectedBank.IsActive = IsActive;

                await _bankRepository.UpdateAsync(SelectedBank);
                await LoadPageAsync(); // refresh current page
                ToastService.Updated();
            }

            FormResetHelper.Reset(this);
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (SelectedBank == null) return;

            if (CustomMessageBox.ShowDelete())
            {
                await _bankRepository.DeleteAsync(SelectedBank.Id);

                await LoadPageAsync(); // reload current page
                FormResetHelper.Reset(this);
                ToastService.Deleted();
            }
        }

        // ===================== On Selection =====================
        partial void OnSelectedBankChanged(Bank value)
        {
            if (value == null) return;

            BankName = value.BankName;
            AccountName = value.AccountName;
            AccountNumber = value.AccountNumber;
            Iban = value.Iban;
            Notes = value.Notes;
            IsActive = value.IsActive;
        }
    }
}
