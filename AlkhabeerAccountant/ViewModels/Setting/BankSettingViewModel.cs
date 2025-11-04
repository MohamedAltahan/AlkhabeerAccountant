using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Core.Validation;
using Alkhabeer.Service.Banks;
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
        public int PageOffset => (CurrentPage - 1) * PageSize;

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


        // ===================== Pagination =====================
        protected override async Task<PaginatedResult<Bank>> GetPagedDataAsync(int page, int size)
        {
            return await _bankService.GetPagedAsync(page, size);
        }

        // ===================== Commands =====================
        [RelayCommand]
        private async Task SaveAsync()
        {
            if (!ValidateFormWithToast())
                return;

            var entity = SelectedItem ?? new Bank();
            entity.BankName = BankName;
            entity.AccountName = AccountName;
            entity.AccountNumber = AccountNumber;
            entity.Iban = Iban;
            entity.Notes = Notes;
            entity.IsActive = IsActive;

            var result = await _bankService.SaveAsync(entity);

            if (result.IsSuccess)
            {
                ToastService.Added();
                CurrentPage = 1;
                await LoadPageAsync();
                FormResetHelper.Reset(this);
            }
            else
            {
                ToastService.Warning(result.ErrorMessage);
            }
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (SelectedItem == null) return;

            if (CustomMessageBox.ShowDelete())
            {
                var result = await _bankService.DeleteAsync(SelectedItem.Id);

                if (result.IsSuccess)
                {
                    await LoadPageAsync();
                    ToastService.Deleted();
                    FormResetHelper.Reset(this);
                }
                else
                {
                    ToastService.Warning(result.ErrorMessage);
                }
            }
        }

        //// ===================== On Selection =====================
        //protected override void OnSelectedItemChanged(Bank value)
        //{
        //    if (value == null) return;
        //    BankName = value.BankName;
        //    AccountName = value.AccountName;
        //    AccountNumber = value.AccountNumber;
        //    Iban = value.Iban;
        //    Notes = value.Notes;
        //    IsActive = value.IsActive;
        //}
    }
}
