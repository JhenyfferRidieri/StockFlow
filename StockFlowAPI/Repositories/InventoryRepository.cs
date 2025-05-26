using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Data;
using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Models;

namespace StockFlowAPI.Repositories
{
    public class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
    {
        public InventoryRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Inventory>> GetAllWithMaterialAsync()
        {
            return await _context.Inventory
                .Include(i => i.Material)
                .OrderBy(i => i.Id)
                .ToListAsync();
        }

        public async Task<Inventory?> GetByIdWithMaterialAsync(int id)
        {
            return await _context.Inventory
                .Include(i => i.Material)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Inventory?> GetByMaterialIdAsync(int materialId)
        {
            return await _context.Inventory
                .Include(i => i.Material)
                .FirstOrDefaultAsync(i => i.MaterialId == materialId);
        }
    }
}
