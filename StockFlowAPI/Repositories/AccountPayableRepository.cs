using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Data;
using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Models;

namespace StockFlowAPI.Repositories
{
    public class AccountPayableRepository : IAccountPayableRepository
    {
        private readonly AppDbContext _context;

        public AccountPayableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountPayable>> GetAllAsync()
        {
            return await _context.AccountsPayable.ToListAsync();
        }

        public async Task<AccountPayable?> GetByIdAsync(int id)
        {
            return await _context.AccountsPayable.FindAsync(id);
        }

        public async Task AddAsync(AccountPayable accountPayable)
        {
            _context.AccountsPayable.Add(accountPayable);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AccountPayable accountPayable)
        {
            _context.AccountsPayable.Update(accountPayable);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsPaidAsync(int id)
        {
            var account = await GetByIdAsync(id);
            if (account != null)
            {
                account.IsPaid = true;
                await UpdateAsync(account);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var account = await GetByIdAsync(id);
            if (account != null)
            {
                _context.AccountsPayable.Remove(account);
                await _context.SaveChangesAsync();
            }
        }
    }
}