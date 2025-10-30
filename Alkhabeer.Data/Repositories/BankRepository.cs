using Alkhabeer.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alkhabeer.Data.Repositories
{
    public class BankRepository
    {
        private readonly DBContext _context;

        public BankRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<Bank>> GetAllAsync()
        {
            return await _context.Banks.AsNoTracking().ToListAsync();
        }
        public async Task AddAsync(Bank bank)
        {
            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Bank bank)
        {
            _context.Banks.Update(bank);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank != null)
            {
                _context.Banks.Remove(bank);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Bank> GetByIdAsync(int id)
        {
            return await _context.Banks.FindAsync(id);
        }
    }
}
