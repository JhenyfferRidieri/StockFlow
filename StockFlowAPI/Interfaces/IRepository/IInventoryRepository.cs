using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IRepository
{
	public interface IInventoryRepository : IRepositoryBase<Inventory>
	{
		Task<IEnumerable<Inventory>> GetAllWithMaterialAsync();
		Task<Inventory?> GetByIdWithMaterialAsync(int id);
		Task<Inventory?> GetByMaterialIdAsync(int materialId);
	}
}
 