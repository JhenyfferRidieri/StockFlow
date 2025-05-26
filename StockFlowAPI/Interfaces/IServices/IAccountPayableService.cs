using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface IAccountPayableService
    {
        Task<IEnumerable<AccountPayable>> GetAllAsync();
        Task<AccountPayable?> GetByIdAsync(int id);
        Task<AccountPayable> CreateAsync(AccountPayable accountPayable);
        Task<AccountPayable> UpdateAsync(AccountPayable accountPayable);
        Task<bool> MarkAsPaidAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}