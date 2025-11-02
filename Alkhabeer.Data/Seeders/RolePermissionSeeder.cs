using Alkhabeer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alkhabeer.Data.Seeders
{
    public static class RolePermissionSeeder
    {
        public static void Seed(DBContext context)
        {
            // ===== Roles =====
            var desiredRoles = new List<Role>
            {
                new Role { Name = "admin", Description = "المدير العام", IsActive = true },
                new Role { Name = "accountant", Description = "محاسب", IsActive = true },
                new Role { Name = "cashier", Description = "أمين خزنة", IsActive = true }
            };

            foreach (var role in desiredRoles)
            {
                var existingRole = context.Roles.FirstOrDefault(r => r.Name == role.Name);

                if (existingRole == null)
                {
                    context.Roles.Add(role);
                }
                else
                {
                    bool updated = false;

                    if (existingRole.Description != role.Description)
                    {
                        existingRole.Description = role.Description;
                        updated = true;
                    }

                    if (existingRole.IsActive != role.IsActive)
                    {
                        existingRole.IsActive = role.IsActive;
                        updated = true;
                    }

                    if (updated)
                        context.Roles.Update(existingRole);
                }
            }

            // ===== Permissions =====
            var desiredPermissions = new List<Permission>
            {
                new Permission { Key = "treasury.view", DisplayName = "عرض الخزائن" },
                new Permission { Key = "treasury.add", DisplayName = "إضافة خزنة" },
                new Permission { Key = "treasury.edit", DisplayName = "تعديل الخزنة" },
                new Permission { Key = "treasury.delete", DisplayName = "حذف الخزنة" },
                new Permission { Key = "users.manage", DisplayName = "إدارة المستخدمين" }
            };

            foreach (var permission in desiredPermissions)
            {
                var existing = context.Permissions.FirstOrDefault(p => p.Key == permission.Key);

                if (existing == null)
                {
                    context.Permissions.Add(permission);
                }
                else
                {
                    bool updated = false;

                    if (existing.DisplayName != permission.DisplayName)
                    {
                        existing.DisplayName = permission.DisplayName;
                        updated = true;
                    }

                    if (updated)
                        context.Permissions.Update(existing);
                }
            }

            context.SaveChanges();

            // ===== Link Admin to all permissions =====
            var admin = context.Roles.FirstOrDefault(r => r.Name == "admin");
            if (admin != null)
            {
                var allPermissions = context.Permissions.ToList();

                foreach (var perm in allPermissions)
                {
                    bool exists = context.RolePermissions
                        .Any(rp => rp.RoleId == admin.Id && rp.PermissionId == perm.Id);

                    if (!exists)
                    {
                        context.RolePermissions.Add(new RolePermission
                        {
                            RoleId = admin.Id,
                            PermissionId = perm.Id
                        });
                    }
                }
            }

            // Save all once
            context.SaveChanges();
        }
    }
}
