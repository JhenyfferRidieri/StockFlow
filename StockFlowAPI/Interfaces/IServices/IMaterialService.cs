using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface IMaterialService
    {
        Task<IEnumerable<Material>> GetAllAsync();
        Task<Material?> GetByIdAsync(int id);
        Task<Material?> GetByNameAsync(string name);
        Task<Material> CreateAsync(Material material);
        Task<Material> UpdateAsync(Material material);
        Task<bool> DeleteAsync(int id);
    }
}
