using StockFlowAPI.Data;
using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StockFlowAPI.Repositories
{
    public class MaterialRepository : RepositoryBase<Material>, IMaterialRepository
    {
        public MaterialRepository(AppDbContext context) : base(context) { }

        public async Task<Material?> GetByNameAsync(string name)
        {
            return await _context.Materials
                .FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> MaterialExistsAsync(int id)
        {
            return await _context.Materials.AnyAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Material>> GetAllOrderedAsync()
        {
            return await _context.Materials
                .OrderBy(m => m.Name)
                .ToListAsync();
        }
    }
}
