using Alkhabeer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Alkhabeer.Data.Repositories
{
    public class SettingRepository
    {
        private readonly DBContext _context;

        public SettingRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<Setting>> GetAllAsync(string? group = null)
        {
            var query = _context.Settings.AsQueryable();

            if (!string.IsNullOrEmpty(group))
                query = query.Where(s => s.Group == group);

            return await query.ToListAsync();
        }

        public async Task<Setting?> GetByKeyAsync(string key)
        {
            return await _context.Settings.FirstOrDefaultAsync(s => s.Key == key);
        }

        public async Task SaveOrUpdateAsync(string key, string value, string? type = null, string? group = null)
        {
            var existing = await _context.Settings.FirstOrDefaultAsync(s => s.Key == key);

            if (existing == null)
            {
                var setting = new Setting
                {
                    Key = key,
                    Value = value,
                    Type = type,
                    Group = group,
                };

                _context.Settings.Add(setting);
            }
            else
            {
                existing.Value = value;
                existing.Type = type;
                existing.Group = group;
            }

            await _context.SaveChangesAsync();
        }
    }
}
