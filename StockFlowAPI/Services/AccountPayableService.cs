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

        public async Task<AccountPayable> CreateAsync(AccountPayable account)
        {
            await _repository.AddAsync(account);
            return account;
        }

        public async Task<AccountPayable> UpdateAsync(AccountPayable account)
        {
            await _repository.UpdateAsync(account);
            return account;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null) return false;

            await _repository.DeleteAsync(account.Id);
            return true;
        }

        public async Task<bool> MarkAsPaidAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null) return false;

            account.IsPaid = true;
            await _repository.UpdateAsync(account);
            return true;
        }

        public async Task<IEnumerable<AccountPayable>> GetByStatusAsync(string status)
        {
            var all = await _repository.GetAllAsync();

            return status.ToLower() switch
            {
                "pendente" => all.Where(a => !a.IsPaid && a.DueDate >= DateTime.Now),
                "atrasado" => all.Where(a => !a.IsPaid && a.DueDate < DateTime.Now),
                "pago" => all.Where(a => a.IsPaid),
                _ => all
            };
        }

        public async Task<IEnumerable<AccountPayable>> GetByCostTypeAsync(string costType)
        {
            var all = await _repository.GetAllAsync();

            if (Enum.TryParse(costType, true, out Models.Enum.CostType parsedType))
            {
                return all.Where(a => a.CostType == parsedType);
            }

            return new List<AccountPayable>();
        }
    }
}
