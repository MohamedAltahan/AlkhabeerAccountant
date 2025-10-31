using Alkhabeer.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        // Get all (no tracking for performance)
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Table.AsNoTracking().ToListAsync();
        }

        //  Get by ID (works for numeric keys)
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }

        //  Add
        public virtual async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        //  Update
        public virtual async Task UpdateAsync(T entity)
        {
            Table.Update(entity);
            await _context.SaveChangesAsync();
        }

        //  Delete by entity
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

        //  Pagination
        public virtual async Task<PaginatedResult<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            if (pageSize <= 0)
                pageSize = 10;

            var query = Table.AsNoTracking()
                .OrderByDescending(e => EF.Property<int>(e, "Id"));

            int total = await query.CountAsync();


            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<T>(data, total, pageNumber, pageSize);
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
