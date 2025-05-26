using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IRepository
{
    public interface IMaterialRepository : IRepositoryBase<Material>
    {
        Task<Material?> GetByNameAsync(string name);
        Task<bool> MaterialExistsAsync(int id);
        Task<IEnumerable<Material>> GetAllOrderedAsync();
    }
}
