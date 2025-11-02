using System;
using System.Linq;
using Alkhabeer.Core.Models;

namespace Alkhabeer.Data.Seeders
{
    public static class DatabaseSeeder
    {
        private const string VersionKey = "app_Version";
        private const string VersionGroup = "system";

        //run all seeders if app version changed
        public static void Seed(DBContext context)
        {
            //  Get current app version (from assembly)
            var currentVersion = "1.0.4";

            //  Get last seeded version from Settings
            var versionSetting = context.Settings
                .FirstOrDefault(s => s.Key == VersionKey && s.Group == VersionGroup);

            string? lastVersion = versionSetting?.Value;

            //  Compare versions
            if (lastVersion == null || !lastVersion.Equals(currentVersion, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[Seeder] App version changed from {lastVersion ?? "none"} → {currentVersion}");
                RunAllSeeders(context);

                //  Update or insert version record
                if (versionSetting == null)
                {
                    versionSetting = new Setting
                    {
                        Key = VersionKey,
                        Group = VersionGroup,
                        Type = "string",
                        Value = currentVersion
                    };
                    context.Settings.Add(versionSetting);
                }
                else
                {
                    versionSetting.Value = currentVersion;
                    context.Settings.Update(versionSetting);
                }

                context.SaveChanges();

                Console.WriteLine($"[Seeder] Database seeding completed for version {currentVersion}");
            }
            else
            {
                Console.WriteLine($"[Seeder] App version unchanged ({currentVersion}) — skipping seeding");
            }
        }

        private static void RunAllSeeders(DBContext context)
        {
            //  Add your specific seeders here
            InventorySettingSeeder.Seed(context);
            RolePermissionSeeder.Seed(context);
            UserSeeder.Seed(context);
        }
    }
}
