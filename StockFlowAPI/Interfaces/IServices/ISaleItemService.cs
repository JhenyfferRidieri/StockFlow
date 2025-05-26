using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface ISaleItemService
    {
        Task<IEnumerable<SaleItem>> GetAllAsync();
        Task<SaleItem?> GetByIdAsync(int id);
        Task<SaleItem> CreateAsync(SaleItem saleItem);
        Task<SaleItem> UpdateAsync(SaleItem saleItem);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId);
    }
}