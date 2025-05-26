using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Data;
using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Models;

namespace StockFlowAPI.Repositories
{
    public class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
    {
        private readonly AppDbContext _context;

        public InventoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetAllWithMaterialAsync()
        {
            return await _context.Inventory
                .Include(i => i.Material) // Faz o join com a tabela de materiais
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
