using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface ICartService
    {
        Task<IEnumerable<SaleItem>> GetCartItemsAsync(int saleId);
        Task<SaleItem> AddItemAsync(int saleId, SaleItem item);
        Task<SaleItem> UpdateItemQuantityAsync(int saleId, int itemId, int quantity);
        Task<bool> RemoveItemAsync(int saleId, int itemId);
    }
}
