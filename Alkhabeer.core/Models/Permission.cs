using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alkhabeer.Core.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Key { get; set; } = string.Empty;   // e.g. Treasury.Delete

        [MaxLength(200)]
        public string? DisplayName { get; set; }          // e.g. "حذف الخزنة"

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
