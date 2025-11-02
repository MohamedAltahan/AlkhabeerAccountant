//using Alkhabeer.Core;
//using Alkhabeer.Core.Models;
//using Alkhabeer.Core.Shared;
//using Alkhabeer.Core.Validation;
//using Alkhabeer.Data.Repositories;
//using AlkhabeerAccountant.CustomControls.SecondaryWindow;
//using AlkhabeerAccountant.Helpers;
//using AlkhabeerAccountant.Services;
//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using System.Collections.ObjectModel;
//using System.ComponentModel.DataAnnotations;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AlkhabeerAccountant.ViewModels.Setting
//{
//    public partial class UsersSettingViewModel : BasePagedViewModel<User>
//    {
//        private readonly UserRepository _userRepository;
//        private readonly RoleRepository _roleRepository;

//        public UsersSettingViewModel(UserRepository UserRepository, RoleRepository RoleRepository) : base(UserRepository)
//        {
//            _userRepository = UserRepository;
//            _roleRepository = RoleRepository;

//            _ = LoadAsync();
//        }

//        // ========== Form Fields ==========

//        [ObservableProperty]
//        [RequiredEx]
//        [MaxLengthEx(100)]
//        private string username = string.Empty;

//        [ObservableProperty]
//        [RequiredEx]
//        [MaxLengthEx(50)]
//        private string fullName = string.Empty;

//        [ObservableProperty]
//        [MaxLengthEx(20)]
//        private string? password;

//        [ObservableProperty]
//        private string? email;

//        [ObservableProperty]
//        private string? phone;

//        [ObservableProperty]
//        private bool isActive = true;

//        [ObservableProperty]
//        private int selectedRoleId;

//        // ========== Collections ==========

//        [ObservableProperty]
//        private ObservableCollection<User> users = new();

//        [ObservableProperty]
//        private ObservableCollection<Role> roles = new();

//        [ObservableProperty]
//        private User? selectedUser;

//        // ========== Commands ==========

//        [RelayCommand]
//        private async Task LoadAsync()
//        {
//            Roles = new ObservableCollection<Role>(await _roleRepository.GetAllAsync());
//            Users = new ObservableCollection<User>(await _userRepository.GetAllWithRolesAsync());
//        }

//        [RelayCommand]
//        private async Task SaveAsync()
//        {
//            if (!ValidateFormWithToast())
//                return;

//            if (SelectedUser == null)
//            {
//                // Add new
//                var user = new User
//                {
//                    Username = Username,
//                    FullName = FullName,
//                    PasswordHash = string.IsNullOrEmpty(Password)
//                        ? HashHelper.HashPassword("123456")
//                        : HashHelper.HashPassword(Password),
//                    Email = Email,
//                    Phone = Phone,
//                    IsActive = IsActive
//                };

//                await _userRepository.AddWithRolesAsync(user, new[] { SelectedRoleId });
//                ToastService.Success();
//            }
//            else
//            {
//                // Update existing
//                SelectedUser.Username = Username;
//                SelectedUser.FullName = FullName;
//                SelectedUser.Email = Email;
//                SelectedUser.Phone = Phone;
//                SelectedUser.IsActive = IsActive;

//                if (!string.IsNullOrEmpty(Password))
//                    SelectedUser.PasswordHash = HashHelper.HashPassword(Password);

//                await _userRepository.UpdateWithRolesAsync(SelectedUser, new[] { SelectedRoleId });
//                ToastService.Updated();
//            }

//            await RefreshListAsync();
//            ClearForm();
//        }

//        [RelayCommand]
//        private async Task DeleteAsync()
//        {
//            if (SelectedUser == null) return;

//            if (CustomMessageBox.ShowDelete())
//            {
//                await _userRepository.DeleteAsync(SelectedUser.Id);
//                ToastService.Success("تم حذف المستخدم");
//                await RefreshListAsync();
//                ClearForm();
//            }
//        }

//        [RelayCommand]
//        private void ClearForm()
//        {
//            Username = string.Empty;
//            FullName = string.Empty;
//            Password = string.Empty;
//            Email = string.Empty;
//            Phone = string.Empty;
//            IsActive = true;
//            SelectedRoleId = 0;
//            SelectedUser = null;
//        }

//        private async Task RefreshListAsync()
//        {
//            Users = new ObservableCollection<User>(await _userRepository.GetAllWithRolesAsync());
//        }

//        partial void OnSelectedUserChanged(User? value)
//        {
//            if (value == null) return;

//            Username = value.Username;
//            FullName = value.FullName;
//            Email = value.Email;
//            Phone = value.Phone;
//            IsActive = value.IsActive;
//            Password = string.Empty; // never show password

//            // Map selected role
//            var role = value.UserRoles.FirstOrDefault()?.Role;
//            SelectedRoleId = role?.Id ?? 0;
//        }
//    }
//}
