using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Data;
using StockFlowAPI.Models;
using StockFlowAPI.Interfaces.IRepository;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly AppDbContext _context;

    public SaleItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SaleItem>> GetAllAsync()
    {
        return await _context.SaleItems.ToListAsync();
    }

    public async Task<SaleItem?> GetByIdAsync(int id)
    {
        return await _context.SaleItems.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<SaleItem> AddAsync(SaleItem saleItem)
    {
        _context.SaleItems.Add(saleItem);
        await _context.SaveChangesAsync();
        return saleItem;
    }

    public async Task<SaleItem> UpdateAsync(SaleItem saleItem)
    {
        _context.SaleItems.Update(saleItem);
        await _context.SaveChangesAsync();
        return saleItem;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var saleItem = await GetByIdAsync(id);
        if (saleItem == null) return false;

        _context.SaleItems.Remove(saleItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId)
    {
        return await _context.SaleItems
            .Where(s => s.SaleId == saleId)
            .ToListAsync();
    }
}
