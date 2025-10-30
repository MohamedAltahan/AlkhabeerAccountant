using Alkhabeer.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Alkhabeer.Data.Seeders
{
    public static class InventorySettingSeeder
    {
        public static void Seed(DBContext context)
        {
            //  Define the desired settings (source of truth)
            var desiredSettings = new List<Setting>
            {
                new Setting { Key = "cost_method", Group = "inventory", Type = "string", Value = "AVG" },
                new Setting { Key = "allow_negative_stock", Group = "inventory", Type = "bool", Value = "true" },
                new Setting { Key = "include_tax_in_cost", Group = "inventory", Type = "bool", Value = "true" }
            };

            foreach (var setting in desiredSettings)
            {
                //  Try to find existing setting by Key + Group
                var existing = context.Settings
                    .FirstOrDefault(s => s.Key == setting.Key && s.Group == setting.Group);

                if (existing == null)
                {
                    // Create new if missing
                    context.Settings.Add(setting);
                }
                else
                {
                    // Update only if value or type changed
                    bool updated = false;

                    if (existing.Value != setting.Value)
                    {
                        existing.Value = setting.Value;
                        updated = true;
                    }

                    if (existing.Type != setting.Type)
                    {
                        existing.Type = setting.Type;
                        updated = true;
                    }

                    if (updated)
                        context.Settings.Update(existing);
                }
            }

            //Save all changes at once
            context.SaveChanges();
        }
    }
}
