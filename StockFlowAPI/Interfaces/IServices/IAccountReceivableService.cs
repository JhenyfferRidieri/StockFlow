using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface IAccountReceivableService
    {
        Task<IEnumerable<AccountReceivable>> GetAllAsync();
        Task<AccountReceivable?> GetByIdAsync(int id);
        Task<AccountReceivable> CreateAsync(AccountReceivable accountReceivable);
        Task<AccountReceivable> UpdateAsync(AccountReceivable accountReceivable);
        Task<bool> DeleteAsync(int id);
    }
}
