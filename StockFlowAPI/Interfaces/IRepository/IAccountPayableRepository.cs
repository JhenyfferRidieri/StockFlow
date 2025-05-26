using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IRepository
{
    public interface IAccountPayableRepository
    {
        Task<IEnumerable<AccountPayable>> GetAllAsync();
        Task<AccountPayable?> GetByIdAsync(int id);
        Task AddAsync(AccountPayable accountPayable);
        Task UpdateAsync(AccountPayable accountPayable);
        Task DeleteAsync(int id);
        Task MarkAsPaidAsync(int id);
    }
}