using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Data;
using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Models;

namespace StockFlowAPI.Repositories
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetAllWithItemsAsync()
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Material)
                .OrderBy(s => s.Id)
                .ToListAsync();
        }

        public async Task<Sale?> GetByIdWithItemsAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Material)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
