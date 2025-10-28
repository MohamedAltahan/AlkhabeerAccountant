using Alkhabeer.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Alkhabeer.Data.Seeders
{
    public static class InventoryCostSettingSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var settings = new List<Setting>
            {
                new Setting { Id = 100, Key = "Cost_method", Group = "Inventory_cost", Type = "string", Value = "FIFO" },
                new Setting { Id = 101, Key = "Allow_negative_stock", Group = "Inventory_cost", Type = "bool", Value = "false" },
                new Setting { Id = 103, Key = "Include_tax_in_cost", Group = "Inventory_cost", Type = "bool", Value = "false" }
            };

            modelBuilder.Entity<Setting>().HasData(settings);
        }
    }
}
