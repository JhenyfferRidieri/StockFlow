using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
    public class AccountPayableService : IAccountPayableService
    {
        private readonly IAccountPayableRepository _repository;

        public AccountPayableService(IAccountPayableRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AccountPayable>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AccountPayable?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<AccountPayable> CreateAsync(AccountPayable accountPayable)
        {
            await _repository.AddAsync(accountPayable);
            return accountPayable;
        }

        public async Task<AccountPayable> UpdateAsync(AccountPayable accountPayable)
        {
            await _repository.UpdateAsync(accountPayable);
            return accountPayable;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> MarkAsPaidAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null) return false;

            await _repository.MarkAsPaidAsync(id);
            return true;
        }
    }
}