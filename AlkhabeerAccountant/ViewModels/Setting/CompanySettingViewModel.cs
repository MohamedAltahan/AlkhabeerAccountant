using Alkhabeer.Core.Models;
using Alkhabeer.Data.Repositories;
using AlkhabeerAccountant.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class CompanySettingViewModel : ObservableObject
    {
        private readonly SettingRepository _repository;
        private readonly ImageService _imageService;

        [ObservableProperty] private string companyName;
        [ObservableProperty] private string companyActivity;
        [ObservableProperty] private string companyLogoPath;
        [ObservableProperty] private BitmapImage? companyLogo;
        [ObservableProperty] private string companyEmail;
        [ObservableProperty] private string companyPhone;
        [ObservableProperty] private string companyAddress;
        [ObservableProperty] private string companyTaxNumber;
        [ObservableProperty] private string companyCommercialRegister;
        [ObservableProperty] private string companyWebsite;
        [ObservableProperty] private string invoiceFooterLine;
        [ObservableProperty] private string invoiceNoteText;

        [ObservableProperty] private bool flag;
        [ObservableProperty] private string statusMessage;

        public CompanySettingViewModel(SettingRepository repository, ImageService imageService)
        {
            _repository = repository;
            _imageService = imageService;
            _ = LoadCompanySettings();
        }

        private async Task LoadCompanySettings()
        {
            Flag = false;
            var settings = await _repository.GetAllAsync("company");

            CompanyName = settings.FirstOrDefault(s => s.Key == "company_name")?.Value ?? "";
            CompanyActivity = settings.FirstOrDefault(s => s.Key == "company_activity")?.Value ?? "";
            CompanyEmail = settings.FirstOrDefault(s => s.Key == "company_email")?.Value ?? "";
            CompanyPhone = settings.FirstOrDefault(s => s.Key == "company_phone")?.Value ?? "";
            CompanyAddress = settings.FirstOrDefault(s => s.Key == "company_address")?.Value ?? "";
            CompanyTaxNumber = settings.FirstOrDefault(s => s.Key == "company_tax_number")?.Value ?? "";
            CompanyCommercialRegister = settings.FirstOrDefault(s => s.Key == "company_commercial_register")?.Value ?? "";
            CompanyWebsite = settings.FirstOrDefault(s => s.Key == "company_website")?.Value ?? "";
            InvoiceFooterLine = settings.FirstOrDefault(s => s.Key == "invoice_footer_line")?.Value ?? "";
            InvoiceNoteText = settings.FirstOrDefault(s => s.Key == "invoice_note_text")?.Value ?? "";
            CompanyLogoPath = settings.FirstOrDefault(s => s.Key == "company_logo")?.Value ?? "";

            if (!string.IsNullOrWhiteSpace(CompanyLogoPath))
            {
                //check existance of the image on the disk
                if (File.Exists(CompanyLogoPath))
                    CompanyLogo = _imageService.LoadImage(CompanyLogoPath);
                else
                    CompanyLogo = null;

                CompanyLogo = _imageService.LoadImage(CompanyLogoPath);
            }
            else
            {
                CompanyLogo = null;
            }

            Flag = true;
        }

        [RelayCommand]
        private async Task Save()
        {
            try
            {
            Flag = false;

            await _repository.SaveOrUpdateAsync("company_name", CompanyName, "string", "company");
            await _repository.SaveOrUpdateAsync("company_activity", CompanyActivity, "string", "company");
            await _repository.SaveOrUpdateAsync("company_email", CompanyEmail, "string", "company");
            await _repository.SaveOrUpdateAsync("company_phone", CompanyPhone, "string", "company");
            await _repository.SaveOrUpdateAsync("company_address", CompanyAddress, "string", "company");
            await _repository.SaveOrUpdateAsync("company_tax_number", CompanyTaxNumber, "string", "company");
            await _repository.SaveOrUpdateAsync("company_commercial_register", CompanyCommercialRegister, "string", "company");
            await _repository.SaveOrUpdateAsync("company_website", CompanyWebsite, "string", "company");
            await _repository.SaveOrUpdateAsync("invoice_footer_line", InvoiceFooterLine, "string", "company");
            await _repository.SaveOrUpdateAsync("invoice_note_text", InvoiceNoteText, "string", "company");

            ToastService.Success();

            Flag = true;
            }
            catch
            {
                ToastService.Error();
            }


        }

        [RelayCommand]
        private async Task UploadLogo()
        {
            var path = await _imageService.ReplaceAsync(CompanyLogoPath, "Company");

            if (!string.IsNullOrEmpty(path))
            {
                CompanyLogoPath = path;
                CompanyLogo = _imageService.LoadImage(path); //  يعرضها فورًا في الواجهة
                await _repository.SaveOrUpdateAsync("company_logo", path, "string", "company");
            }
        }

        [RelayCommand]
        private void RemoveLogo()
        {
            if (_imageService.Remove(CompanyLogoPath))
            {
                CompanyLogo = null;
                CompanyLogoPath = string.Empty;
            }
        }
    }
}
