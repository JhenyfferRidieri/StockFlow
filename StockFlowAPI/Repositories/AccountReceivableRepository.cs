using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Data;
using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Models;

namespace StockFlowAPI.Repositories
{
    public class AccountReceivableRepository : IAccountReceivableRepository
    {
        private readonly AppDbContext _context;

        public AccountReceivableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountReceivable>> GetAllAsync()
        {
            return await _context.AccountsReceivable
                .Include(a => a.Sale)
                .ToListAsync();
        }

        public async Task<AccountReceivable?> GetByIdAsync(int id)
        {
            return await _context.AccountsReceivable
                .Include(a => a.Sale)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(AccountReceivable accountReceivable)
        {
            _context.AccountsReceivable.Add(accountReceivable);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AccountReceivable accountReceivable)
        {
            _context.AccountsReceivable.Update(accountReceivable);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var account = await GetByIdAsync(id);
            if (account != null)
            {
                _context.AccountsReceivable.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AccountReceivable?> GetBySaleIdAsync(int saleId)
        {
            return await _context.AccountsReceivable
                .FirstOrDefaultAsync(a => a.SaleId == saleId);
        }

    }
}
