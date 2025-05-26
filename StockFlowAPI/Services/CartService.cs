using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
    public class CartService : ICartService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleItemRepository _saleItemRepository;

        public CartService(ISaleRepository saleRepository, ISaleItemRepository saleItemRepository)
        {
            _saleRepository = saleRepository;
            _saleItemRepository = saleItemRepository;
        }

        public async Task<IEnumerable<SaleItem>> GetCartItemsAsync(int saleId)
        {
            var sale = await _saleRepository.GetByIdWithItemsAsync(saleId);
            return sale?.SaleItems ?? new List<SaleItem>();
        }

        public async Task<SaleItem> AddItemAsync(int saleId, SaleItem item)
        {
            item.SaleId = saleId;
            await _saleItemRepository.AddAsync(item);
            await UpdateSaleTotalAsync(saleId);
            return item;
        }

        public async Task<SaleItem> UpdateItemQuantityAsync(int saleId, int itemId, int quantity)
        {
            var item = await _saleItemRepository.GetByIdAsync(itemId);
            if (item == null || item.SaleId != saleId)
                throw new Exception("Item não encontrado no carrinho.");

            item.Quantity = quantity;
            await _saleItemRepository.UpdateAsync(item);
            await UpdateSaleTotalAsync(saleId);
            return item;
        }

        public async Task<bool> RemoveItemAsync(int saleId, int itemId)
        {
            var item = await _saleItemRepository.GetByIdAsync(itemId);
            if (item == null || item.SaleId != saleId)
                return false;

            await _saleItemRepository.DeleteAsync(item.Id);
            await UpdateSaleTotalAsync(saleId);
            return true;
        }

        private async Task UpdateSaleTotalAsync(int saleId)
        {
            var sale = await _saleRepository.GetByIdWithItemsAsync(saleId);
            if (sale != null)
            {
                sale.Total = sale.SaleItems.Sum(i => i.Quantity * i.UnitPrice);
                await _saleRepository.UpdateAsync(sale);
            }
        }
    }
}