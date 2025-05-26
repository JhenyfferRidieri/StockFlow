using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface ISaleStateService
    {
        Task<bool> UpdateStatusAsync(int saleId, string newStatus);
    }
}
