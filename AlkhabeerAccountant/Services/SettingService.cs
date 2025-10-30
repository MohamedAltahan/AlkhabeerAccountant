using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alkhabeer.Data;

namespace AlkhabeerAccountant.Services
{
    public class SettingService
    {
        private readonly DBContext _context;

        public SettingService(DBContext context)
        {
            _context = context;
        }

        // 🔹 Get inventory settings (async)
        public async Task<Dictionary<string, string?>> GetInventorySettingsAsync()
        {
            return await _context.Settings
                .Where(s => s.Group == "inventory")
                .ToDictionaryAsync(s => s.Key, s => s.Value);
        }

        // 🔹 Update a single inventory setting (async)
        public async Task UpdateInventorySettingAsync(string key, string value)
        {
            var setting = await _context.Settings
                .FirstOrDefaultAsync(s => s.Group == "inventory" && s.Key == key);

            if (setting != null)
            {
                setting.Value = value;
                _context.Settings.Update(setting);
                await _context.SaveChangesAsync();
            }
        }
    }
}
