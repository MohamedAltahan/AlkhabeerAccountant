using Alkhabeer.Core.Models;
using Alkhabeer.Core.Validation;
using Alkhabeer.Data.Repositories;
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
    public partial class BankSettingViewModel : BaseViewModel
    {
        private readonly BankRepository _bankRepository;

        public BankSettingViewModel(BankRepository bankRepository)
        {
            _bankRepository = bankRepository;
            LoadBanksAsync();
        }

        //  Form fields
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

        //  Table data
        [ObservableProperty] private ObservableCollection<Bank> banks = new();
        [ObservableProperty] private Bank selectedBank;

        //  Load all banks
        private async Task LoadBanksAsync()
        {
            var allBanks = await _bankRepository.GetAllAsync();
            Banks = new ObservableCollection<Bank>(allBanks);
        }

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
                Banks.Add(newBank);
                ToastService.Added();
            }
            else
            {
                // Update the existing bank
                SelectedBank.BankName = BankName;
                SelectedBank.AccountName = AccountName;
                SelectedBank.AccountNumber = AccountNumber;
                SelectedBank.Iban = Iban;
                SelectedBank.Notes = Notes;
                SelectedBank.IsActive = IsActive;

                await _bankRepository.UpdateAsync(SelectedBank);
                ToastService.Updated();
            }
            ClearForm();
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (SelectedBank == null) return;

            await _bankRepository.DeleteAsync(SelectedBank.Id);
            Banks.Remove(SelectedBank);
            ClearForm();
            ToastService.Deleted();
        }
        private void ClearForm()
        {
            BankName = string.Empty;
            AccountName = string.Empty;
            AccountNumber = string.Empty;
            Iban = string.Empty;
            Notes = string.Empty;
            IsActive = true;
            SelectedBank = null;
        }

        partial void OnSelectedBankChanged(Bank value)
        {
            if (value != null)
            {
                BankName = value.BankName;
                AccountName = value.AccountName;
                AccountNumber = value.AccountNumber;
                Iban = value.Iban;
                Notes = value.Notes;
                IsActive = value.IsActive;
            }
        }


    }
}
