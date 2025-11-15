using Alkhabeer.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlkhabeerAccountant.ViewModels.Setting
{
    public partial class PermissionSelectable : ObservableObject
    {
        public Permission? Permission { get; set; }

        [ObservableProperty]
        private bool isSelected;

        public string Name => Permission.DisplayName;
        public string Group => Permission.Key.Split('.')[0];
        public int Id => Permission.Id;

    }

    public partial class PermissionsDialogViewModel : BaseViewModel<PermissionsDialogViewModel>
    {
        public ObservableCollection<IGrouping<string, PermissionSelectable>> GroupedPermissions { get; }

        public PermissionsDialogViewModel(IEnumerable<Permission> all, IEnumerable<int> selected)
        {
            var list = all.Select(p => new PermissionSelectable
            {
                Permission = p,
                IsSelected = selected.Contains(p.Id)
            });

            GroupedPermissions = new ObservableCollection<IGrouping<string, PermissionSelectable>>(
                list.GroupBy(x => x.Group)
            );
        }

        // ✔ FIXED: Flatten grouped permissions and return selected IDs
        public IList<int> GetSelectedIds()
        {
            return GroupedPermissions
                .SelectMany(group => group)          // flatten groups
                .Where(p => p.IsSelected)            // only selected
                .Select(p => p.Id)                   // return permission IDs
                .ToList();
        }

        public Action<bool>? CloseDialog { get; set; }

        [RelayCommand]
        private void Confirm() => CloseDialog?.Invoke(true);

        [RelayCommand]
        private void Cancel() => CloseDialog?.Invoke(false);
    }
}
