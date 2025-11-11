using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Alkhabeer.Data.Seeders
{
    public static class UserSeeder
    {
        public static void Seed(DBContext context)
        {
            // ===== Find the Admin role =====
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "admin");
            if (adminRole == null)
                return; // must seed roles first

            // ===== Define the desired Admin user =====
            var desiredAdmin = new User
            {
                Username = "admin",
                FullName = "مدير النظام",
                Email = "admin@alkhabeer.local",
                Phone = "01000000000",
                PasswordHash = HashHelper.HashPassword("123456"), // default password
                IsActive = true
            };

            // ===== Check if user exists =====
            var existing = context.Users.FirstOrDefault(u => u.Username == desiredAdmin.Username);

            if (existing == null)
            {
                // Create new user
                context.Users.Add(desiredAdmin);
                context.SaveChanges();

            }
            else
            {
                bool updated = false;

                if (existing.FullName != desiredAdmin.FullName)
                {
                    existing.FullName = desiredAdmin.FullName;
                    updated = true;
                }

                if (existing.Email != desiredAdmin.Email)
                {
                    existing.Email = desiredAdmin.Email;
                    updated = true;
                }

                if (existing.Phone != desiredAdmin.Phone)
                {
                    existing.Phone = desiredAdmin.Phone;
                    updated = true;
                }

                if (!existing.IsActive)
                {
                    existing.IsActive = true;
                    updated = true;
                }

                // Optionally refresh password if blank
                if (string.IsNullOrEmpty(existing.PasswordHash))
                {
                    existing.PasswordHash = HashHelper.HashPassword("123456");
                    updated = true;
                }

                if (updated)
                    context.Users.Update(existing);

            }

            // Save all once
            context.SaveChanges();
        }
    }
}
