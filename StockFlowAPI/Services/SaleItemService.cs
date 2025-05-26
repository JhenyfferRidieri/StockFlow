using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
    public class SaleItemService : ISaleItemService
    {
        private readonly ISaleItemRepository _saleItemRepository;

        public SaleItemService(ISaleItemRepository saleItemRepository)
        {
            _saleItemRepository = saleItemRepository;
        }

        public async Task<IEnumerable<SaleItem>> GetAllAsync()
        {
            return await _saleItemRepository.GetAllAsync();
        }

        public async Task<SaleItem?> GetByIdAsync(int id)
        {
            return await _saleItemRepository.GetByIdAsync(id);
        }

        public async Task<SaleItem> CreateAsync(SaleItem saleItem)
        {
            return await _saleItemRepository.AddAsync(saleItem);
        }

        public async Task<SaleItem> UpdateAsync(SaleItem saleItem)
        {
            return await _saleItemRepository.UpdateAsync(saleItem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _saleItemRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId)
        {
            return await _saleItemRepository.GetBySaleIdAsync(saleId);
        }
    }
}
