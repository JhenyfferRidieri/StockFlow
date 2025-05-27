using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface IAccountReceivableService
    {
        Task<IEnumerable<AccountReceivable>> GetAllAsync();
        Task<AccountReceivable?> GetByIdAsync(int id);
        Task<AccountReceivable> CreateAsync(AccountReceivable account);
        Task<AccountReceivable> UpdateAsync(AccountReceivable account);
        Task<bool> DeleteAsync(int id);

        Task<bool> MarkAsReceivedAsync(int id);
        Task<IEnumerable<AccountReceivable>> GetByStatusAsync(string status);
        Task<AccountReceivable> GenerateFromSaleAsync(Sale sale);
    }
}
