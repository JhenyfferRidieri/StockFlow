using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
    public class AccountReceivableService : IAccountReceivableService
    {
        private readonly IAccountReceivableRepository _repository;

        public AccountReceivableService(IAccountReceivableRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AccountReceivable>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AccountReceivable?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<AccountReceivable> CreateAsync(AccountReceivable account)
        {
            await _repository.AddAsync(account);
            return account;
        }

        public async Task<AccountReceivable> UpdateAsync(AccountReceivable account)
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

        public async Task<bool> MarkAsReceivedAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null) return false;

            account.IsReceived = true;
            await _repository.UpdateAsync(account);
            return true;
        }

        public async Task<IEnumerable<AccountReceivable>> GetByStatusAsync(string status)
        {
            var all = await _repository.GetAllAsync();

            return status.ToLower() switch
            {
                "pendente" => all.Where(a => !a.IsReceived && a.DueDate >= DateTime.Now),
                "vencido" => all.Where(a => !a.IsReceived && a.DueDate < DateTime.Now),
                "pago" => all.Where(a => a.IsReceived),
                _ => all
            };
        }

        public async Task<AccountReceivable> GenerateFromSaleAsync(Sale sale)
        {
            var account = new AccountReceivable
            {
                Description = $"Recebível da Venda #{sale.Id} - {sale.CustomerName}",
                Amount = sale.Total,
                DueDate = DateTime.Now.AddDays(7), // Vencimento padrão 7 dias
                IsReceived = false,
                SaleId = sale.Id
            };

            await _repository.AddAsync(account);
            return account;
        }
    }
}
