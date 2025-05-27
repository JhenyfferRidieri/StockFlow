using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Data;
using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Models;

namespace StockFlowAPI.Repositories
{
    public class InventoryMovementRepository : RepositoryBase<InventoryMovement>, IInventoryMovementRepository
    {
        public InventoryMovementRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<InventoryMovement>> GetAllWithMaterialAsync()
        {
            return await _context.InventoryMovements
                .Include(i => i.Material)
                .OrderByDescending(i => i.Date)
                .ToListAsync();
        }

        public async Task<InventoryMovement?> GetByIdWithMaterialAsync(int id)
        {
            return await _context.InventoryMovements
                .Include(i => i.Material)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<InventoryMovement>> GetByMaterialIdAsync(int materialId)
        {
            return await _context.InventoryMovements
                .Include(i => i.Material)
                .Where(i => i.MaterialId == materialId)
                .OrderByDescending(i => i.Date)
                .ToListAsync();
        }
    }
}
