using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface IInventoryMovementService
    {
        Task<IEnumerable<InventoryMovement>> GetAllAsync();
        Task<InventoryMovement?> GetByIdAsync(int id);
        Task<IEnumerable<InventoryMovement>> GetByMaterialIdAsync(int materialId);
        Task<InventoryMovement> CreateAsync(InventoryMovement movement);
    }
}
