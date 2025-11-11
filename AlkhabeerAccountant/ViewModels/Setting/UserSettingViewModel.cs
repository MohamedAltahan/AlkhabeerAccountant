using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Core.Validation;
using Alkhabeer.Services;
using AlkhabeerAccountant.CustomControls.SecondaryWindow;
using AlkhabeerAccountant.Helpers;
using AlkhabeerAccountant.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class UserSettingViewModel : BasePagedViewModel<User>
    {
        private readonly UserService _userService;

        [RequiredEx, MaxLengthEx(50), ObservableProperty]
        private string fullName = string.Empty;
        [RequiredEx, MaxLengthEx(50), ObservableProperty]
        private string username = string.Empty;
        [ObservableProperty]
        private string email = string.Empty;
        [ObservableProperty]
        private string phone = string.Empty;
        [ObservableProperty]
        private string passwordHash = string.Empty;
        [ObservableProperty]
        private bool isActive = true;
        // Selected role form combobox
        [ObservableProperty]
        private int roleId;
        // All roles loaded from backend
        [ObservableProperty] private ObservableCollection<Role> roles = new();


        public UserSettingViewModel(UserService userService) : base(userService)
        {
            _userService = userService;
            LoadAsync();
        }

        // ================== Load ==================
        [RelayCommand]
        private async Task LoadAsync()
        {
            var usersResult = await _userService.GetAllWithRolesAsync();
            if (usersResult.IsSuccess)
                Items = new ObservableCollection<User>(usersResult.Value!);

            var rolesResult = await _userService.GetRolesAsync();
            if (rolesResult.IsSuccess)
                Roles = new ObservableCollection<Role>(rolesResult.Value!);
        }

        //// ================== Save or Update ==================
        [RelayCommand]
        protected async override Task SaveOrUpdateAsync()
        {
            if (!ValidateForm()) return;

            var entity = MapEntityFromView();

            if (!string.IsNullOrWhiteSpace(PasswordHash))
                entity.PasswordHash = HashHelper.HashPassword(PasswordHash);


            var result = await _userService.SaveOrUpdateAsync(entity);

            if (result.IsSuccess)
            {
                ToastService.Success();
                await LoadAsync();
                FormResetHelper.Reset(this);
            }
            else
            {
                ToastService.Error();
            }
        }

        // ================== Mapping ==================
        protected override User MapEntityFromView()
        {
            var entity = SelectedItem ?? new User();
            entity.FullName = FullName;
            entity.Username = Username;
            entity.Email = Email;
            entity.Phone = Phone;
            entity.IsActive = IsActive;
            entity.PasswordHash = PasswordHash;
            entity.RoleId = RoleId;
            return entity;
        }

    }
}
