using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IRepository
{
    public interface ISaleItemRepository
    {
        Task<IEnumerable<SaleItem>> GetAllAsync();
        Task<SaleItem?> GetByIdAsync(int id);
        Task<SaleItem> AddAsync(SaleItem saleItem);
        Task<SaleItem> UpdateAsync(SaleItem saleItem);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId);
    }
}