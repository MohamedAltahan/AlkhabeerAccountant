using AlkhabeerAccountant.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class InventorySettingViewModel : BaseViewModel<object>
    {
        private readonly SettingService _settingService;

        // 🔹 Properties bound to UI
        [ObservableProperty] private string costMethod = "AVG";
        [ObservableProperty] private bool allowNegativeStock = false;
        [ObservableProperty] private bool includeTaxInCost = false;

        // 🔹 Constructor
        public InventorySettingViewModel(SettingService settingService)
        {
            _settingService = settingService;
            _ = LoadSettingsAsync(); // fire & forget since constructor can't be async
        }

        // 🔹 Load existing settings from database
        private async Task LoadSettingsAsync()
        {
            // get dictionary from async service call
            var settings = await _settingService.GetInventorySettingsAsync();

            if (settings.TryGetValue("cost_method", out var method))
                CostMethod = method;
            if (settings.TryGetValue("allow_negative_stock", out var neg))
                AllowNegativeStock = bool.Parse(neg);
            if (settings.TryGetValue("include_tax_in_cost", out var tax))
                IncludeTaxInCost = bool.Parse(tax);
        }

        // 🔹 Save command (for Save button)
        [RelayCommand]
        private async Task SaveAsync()
        {
            await _settingService.UpdateInventorySettingAsync("cost_method", CostMethod);
            await _settingService.UpdateInventorySettingAsync("allow_negative_stock", AllowNegativeStock.ToString());
            await _settingService.UpdateInventorySettingAsync("include_tax_in_cost", IncludeTaxInCost.ToString());

            ToastService.Success();
        }
    }
}
