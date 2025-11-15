using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Core.Validation;
using Alkhabeer.Service;
using Alkhabeer.Services;
using AlkhabeerAccountant.CustomControls.SecondaryWindow;
using AlkhabeerAccountant.Views.Setting;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class RolesSettingViewModel : BasePagedViewModel<Role>
    {
        private new readonly RoleService _service;
        public RolesSettingViewModel(RoleService service) : base(service)
        {
            _service = service;
            _ = LoadPageAsync();
        }

        [ObservableProperty, RequiredEx]
        private string name = string.Empty;
        [ObservableProperty]
        private string? description = string.Empty;
        [ObservableProperty]
        private bool isActive = true;
        // Permissions list (checkbox)
        [ObservableProperty] private ObservableCollection<Permission> permissions = new();
        public List<int> SelectedPermissionIds { get; set; } = new();

        protected override async Task<PaginatedResult<Role>> GetPagedDataAsync(int pageNumber, int pageSize)
        {
            return await _service.GetPagedWithPermissionsAsync(pageNumber, pageSize);
        }

        [RelayCommand]
        protected async Task SaveOrUpdateAsync()
        {
            if (!ValidateForm()) return;

            var entity = SelectedItem ?? new Role();
            entity.Name = Name;
            entity.Description = Description;
            entity.IsActive = IsActive;

            var result = await _service.SaveOrUpdateWithPermissionsAsync(entity, SelectedPermissionIds);
            await CheckSaveResultAsync(result);
        }

        [RelayCommand]
        protected async Task DeleteAsync()
        {
            if (SelectedItem == null) return;
            if (!CustomMessageBox.ShowDelete()) return;
            var result = await _service.DeleteAsync(SelectedItem.Id);
            await CheckDeleteResultAsync(result);
        }


        [RelayCommand]
        private async void OpenPermissionsDialog()
        {
            var allPermissions = await _service.GetAllPermissionsAsync();

            IEnumerable<int> selectedIds;

            // RULE 1: If the user already selected something in memory,use SelectedPermissionIds
            if (SelectedPermissionIds != null && (SelectedPermissionIds.Count() > 0))
            {
                selectedIds = SelectedPermissionIds;
            }
            // RULE 2: Else if editing an existing role, load from database
            else if (SelectedItem != null && SelectedItem.RolePermissions != null)
            {
                selectedIds = SelectedItem.RolePermissions.Select(rp => rp.PermissionId);
            }
            // RULE 3: New role → nothing selected
            else
            {
                selectedIds = Enumerable.Empty<int>();
            }

            var vm = new PermissionsDialogViewModel(allPermissions, selectedIds);

            var dialog = new PermissionsDialogWindow
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };

            bool confirmed = false;
            vm.CloseDialog = confirmedResult =>
            {
                confirmed = confirmedResult;
                dialog.Close();
            };

            dialog.ShowDialog();

            if (confirmed)
            {
                // Save selected into ViewModel (in-memory)
                SelectedPermissionIds = vm.GetSelectedIds().ToList();

                // OPTIONAL: If editing an existing role,
                // update UI model so re-open shows correct checks
                if (SelectedItem != null)
                {
                    SelectedItem.RolePermissions = SelectedPermissionIds
                        .Select(id => new RolePermission
                        {
                            RoleId = SelectedItem.Id,
                            PermissionId = id
                        }).ToList();
                }
            }
        }

    }
}
