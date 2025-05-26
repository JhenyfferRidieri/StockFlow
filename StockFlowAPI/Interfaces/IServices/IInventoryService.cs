using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<Inventory?> GetByIdAsync(int id);
        Task<Inventory?> GetByMaterialIdAsync(int materialId);
        Task<Inventory> CreateAsync(Inventory inventory);
        Task<Inventory> UpdateAsync(Inventory inventory);
        Task<bool> DeleteAsync(int id);
    }
}
