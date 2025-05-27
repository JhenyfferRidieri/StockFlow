using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllAsync();
        Task<Sale?> GetByIdAsync(int id);
        Task<Sale> CreateAsync(Sale sale);
        Task<Sale> UpdateAsync(Sale sale);
        Task<bool> DeleteAsync(int id);
        Task<bool> PaySaleAsync(int saleId);
        Task<bool> CancelSaleAsync(int saleId, string reason);
        Task<bool> DecreaseStockForSaleAsync(Sale sale);
    }
}
