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
using System.Diagnostics;
using System.Threading.Tasks;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class BankSettingViewModel : BasePagedViewModel<Bank>
    {
        private readonly BankService _bankService;

        public BankSettingViewModel(BankService bankService) : base(bankService)
        {
            _bankService = bankService;
            _ = LoadPageAsync();
        }

        // ===================== Form Fields =====================
        [ObservableProperty]
        [RequiredEx, MaxLengthEx(50)]
        private string bankName;

        [ObservableProperty]
        [RequiredEx, MaxLengthEx(50)]
        private string accountName;

        [ObservableProperty]
        [RequiredEx, MaxLengthEx(25), NumbersOnlyEx]
        private string accountNumber;

        [ObservableProperty]
        [MaxLengthEx(25), NumbersOnlyEx]
        private string? iban;

        [ObservableProperty]
        [MaxLengthEx(300)]
        private string? notes;

        [ObservableProperty]
        private bool isActive = true;

        protected override Bank MapEntityFromView()
        {
            var entity = SelectedItem ?? new Bank();
            entity.BankName = BankName;
            entity.AccountName = AccountName;
            entity.AccountNumber = AccountNumber;
            entity.Iban = Iban;
            entity.Notes = Notes;
            entity.IsActive = IsActive;
            return entity;
        }


    }
}
