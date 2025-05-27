using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IRepository
{
    public interface IInventoryMovementRepository : IRepositoryBase<InventoryMovement>
    {
        Task<IEnumerable<InventoryMovement>> GetAllWithMaterialAsync();
        Task<InventoryMovement?> GetByIdWithMaterialAsync(int id);
        Task<IEnumerable<InventoryMovement>> GetByMaterialIdAsync(int materialId);
    }
}
