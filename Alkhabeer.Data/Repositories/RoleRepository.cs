using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkhabeer.Data.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(DBContext context) : base(context)
        {
        }

        // ============= Custom Queries =============

        // Get all roles with permissions (no tracking)
        public async Task<List<Role>> GetAllWithPermissionsAsync()
        {
            return await Table
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .AsNoTracking()
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }

        // Get single role with permissions
        public async Task<Role?> GetByIdWithPermissionsAsync(int id)
        {
            return await Table
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // Add role with optional permission list
        public async Task AddWithPermissionsAsync(Role role, IEnumerable<int>? permissionIds = null)
        {
            if (permissionIds != null)
            {
                role.RolePermissions = permissionIds.Select(pid => new RolePermission
                {
                    PermissionId = pid
                }).ToList();
            }

            await Table.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        // Update role and reassign permissions
        public async Task UpdateWithPermissionsAsync(Role role, IEnumerable<int>? permissionIds = null)
        {
            var existing = await Table
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == role.Id);

            if (existing == null)
                return;

            existing.Name = role.Name;
            existing.Description = role.Description;
            existing.IsActive = role.IsActive;

            if (permissionIds != null)
            {
                // remove old
                var old = _context.Set<RolePermission>().Where(rp => rp.RoleId == role.Id);
                _context.Set<RolePermission>().RemoveRange(old);

                // add new
                foreach (var pid in permissionIds)
                {
                    _context.Set<RolePermission>().Add(new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = pid
                    });
                }
            }

            Table.Update(existing);
            await _context.SaveChangesAsync();
        }

        // Delete role (and related RolePermissions via cascade)
        public override async Task DeleteAsync(int id)
        {
            var entity = await Table.FindAsync(id);
            if (entity == null)
                return;

            Table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        // Get all permissions (for UI combo/dialog)
        public async Task<List<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Set<Permission>()
                .AsNoTracking()
                .OrderBy(p => p.Key)
                .ToListAsync();
        }

        // Get paginated roles with permissions
        public async Task<PaginatedResult<Role>> GetPagedWithPermissionsAsync(int page, int pageSize)
        {
            var query = Table
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .AsNoTracking()
                .OrderByDescending(r => r.Id);

            return await GetPagedAsync(query, page, pageSize);
        }
    }
}
