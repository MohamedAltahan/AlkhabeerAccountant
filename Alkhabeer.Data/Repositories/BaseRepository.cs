using Alkhabeer.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alkhabeer.Data.Repositories
{
    public class BaseRepository<T> where T : class
    {
        protected readonly DBContext _context;
        protected DbSet<T> Table => _context.Set<T>();

        public BaseRepository(DBContext context)
        {
            _context = context;
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Table.AsNoTracking().ToListAsync();
        }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await Table
                .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public virtual async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T entity)
        {
            // Find tracked entity with the same Id and detach it
            var keyProperty = typeof(T).GetProperty("Id");
            if (keyProperty != null)
            {
                var entityId = keyProperty.GetValue(entity);
                var local = _context.Set<T>()
                    .Local
                    .FirstOrDefault(e => keyProperty.GetValue(e).Equals(entityId));

                if (local != null)
                    _context.Entry(local).State = EntityState.Detached;
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            Table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        //  Delete by ID (optional convenience)
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        //  ==========================Pagination=========================
        public virtual async Task<PaginatedResult<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = Table.AsNoTracking()
                .OrderByDescending(e => EF.Property<int>(e, "Id"));

            int totalCount = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<T>(data, totalCount, pageNumber, pageSize);
        }

        //pagination wiht filter(inject query)
        public async Task<PaginatedResult<T>> GetPagedAsync(IQueryable<T> query, int pageNumber, int pageSize)
        {
            int total = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<T>(data, total, pageNumber, pageSize);
        }

        //allow linq chaining
        public IQueryable<T> GetQueryable()
        {
            return Table.AsNoTracking();
        }
    }
}
