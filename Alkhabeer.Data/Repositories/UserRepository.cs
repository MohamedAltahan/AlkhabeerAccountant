using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkhabeer.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DBContext context) : base(context) { }

        // ============= Custom Queries =============

        // Get all users with their roles
        public async Task<List<User>> GetAllWithRolesAsync()
        {
            return await Table
                .Include(u => u.Role)
                .AsNoTracking()
                .OrderByDescending(u => u.Id)
                .ToListAsync();
        }

        // Get user by ID with roles
        public async Task<User?> GetByIdWithRolesAsync(int id)
        {
            return await Table
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Add new user with roles
        public async Task AddWithRolesAsync(User user, IEnumerable<int>? roleIds = null)
        {


            await Table.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Update user and roles
        public async Task UpdateWithRolesAsync(User user, IEnumerable<int>? roleIds = null)
        {
            var existing = await Table
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existing == null)
                return;

            // Basic fields
            existing.Username = user.Username;
            existing.FullName = user.FullName;
            existing.Email = user.Email;
            existing.Phone = user.Phone;
            existing.IsActive = user.IsActive;

            // Update password if provided
            if (!string.IsNullOrEmpty(user.PasswordHash))
                existing.PasswordHash = user.PasswordHash;

            Table.Update(existing);
            await _context.SaveChangesAsync();
        }

        // Delete user (with cascade on UserRoles)
        public override async Task DeleteAsync(int id)
        {
            var entity = await Table.FindAsync(id);
            if (entity == null)
                return;

            Table.Remove(entity);
            await _context.SaveChangesAsync();
        }


        // Assign specific roles to user (replaces all existing)
        //public async Task AssignRolesAsync(int userId, IEnumerable<int> roleIds)
        //{
        //    var existing = _context.Set<UserRole>().Where(ur => ur.UserId == userId);
        //    _context.Set<UserRole>().RemoveRange(existing);

        //    foreach (var rid in roleIds)
        //    {
        //        _context.Set<UserRole>().Add(new UserRole
        //        {
        //            UserId = userId,
        //            RoleId = rid
        //        });
        //    }

        //    await _context.SaveChangesAsync();
        //}

        // Get paginated users with roles
        public async Task<PaginatedResult<User>> GetPagedWithRolesAsync(int page, int pageSize)
        {
            var query = Table
                .AsNoTracking()
                .OrderByDescending(u => u.Id);

            return await GetPagedAsync(query, page, pageSize);
        }
    }
}
