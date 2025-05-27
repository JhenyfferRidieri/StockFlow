using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IRepository
{
    public interface IAccountReceivableRepository
    {
        Task<IEnumerable<AccountReceivable>> GetAllAsync();
        Task<AccountReceivable?> GetBySaleIdAsync(int saleId);
        Task<AccountReceivable?> GetByIdAsync(int id);
        Task AddAsync(AccountReceivable accountReceivable);
        Task UpdateAsync(AccountReceivable accountReceivable);
        Task DeleteAsync(int id);
    }
}
