using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface IAccountPayableService
    {
        Task<IEnumerable<AccountPayable>> GetAllAsync();
        Task<AccountPayable?> GetByIdAsync(int id);
        Task<AccountPayable> CreateAsync(AccountPayable account);
        Task<AccountPayable> UpdateAsync(AccountPayable account);
        Task<bool> DeleteAsync(int id);

        Task<bool> MarkAsPaidAsync(int id);
        Task<IEnumerable<AccountPayable>> GetByStatusAsync(string status);
        Task<IEnumerable<AccountPayable>> GetByCostTypeAsync(string costType);
    }
}
