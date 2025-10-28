//using AlkhabeerAccountant.Services;
//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace AlkhabeerAccountant.ViewModels.Setting
//{
//    public partial class InventoryCostSettingViewModel : ObservableObject
//    {
//        private readonly SettingService _settingService;

//        // 🔹 Properties bound to UI
//        [ObservableProperty]
//        private string costMethod = "FIFO";

//        [ObservableProperty]
//        private bool allowNegativeStock;

//        [ObservableProperty]
//        private int roundingPrecision = 2;

//        [ObservableProperty]
//        private bool includeFreightInCost = true;

//        [ObservableProperty]
//        private bool includeTaxInCost;

//        // 🔹 Constructor
//        public InventoryCostSettingViewModel(SettingService settingService)
//        {
//            _settingService = settingService;
//            _ = LoadSettingsAsync();
//        }

//        // 🔹 Load existing settings from database
//        private async Task LoadSettingsAsync()
//        {
//            var settings = _settingService.GetInventoryCostSettings();

//            if (settings.TryGetValue("CostMethod", out var method)) CostMethod = method;
//            if (settings.TryGetValue("AllowNegativeStock", out var neg)) AllowNegativeStock = bool.Parse(neg);
//            if (settings.TryGetValue("RoundingPrecision", out var round)) RoundingPrecision = int.Parse(round);
//            if (settings.TryGetValue("IncludeFreightInCost", out var freight)) IncludeFreightInCost = bool.Parse(freight);
//            if (settings.TryGetValue("IncludeTaxInCost", out var tax)) IncludeTaxInCost = bool.Parse(tax);
//        }

//        // 🔹 Save command (for Save button)
//        [RelayCommand]
//        private async Task SaveAsync()
//        {
//            await _settingService.UpdateInventoryCostSettingAsync("CostMethod", CostMethod);
//            await _settingService.UpdateInventoryCostSettingAsync("AllowNegativeStock", AllowNegativeStock.ToString());
//            await _settingService.UpdateInventoryCostSettingAsync("RoundingPrecision", RoundingPrecision.ToString());
//            await _settingService.UpdateInventoryCostSettingAsync("IncludeFreightInCost", IncludeFreightInCost.ToString());
//            await _settingService.UpdateInventoryCostSettingAsync("IncludeTaxInCost", IncludeTaxInCost.ToString());
//        }
//    }
//}
