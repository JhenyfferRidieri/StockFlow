using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _saleRepository.GetAllWithItemsAsync();
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _saleRepository.GetByIdWithItemsAsync(id);
        }

        public async Task<Sale> CreateAsync(Sale sale)
        {
            await _saleRepository.AddAsync(sale);
            return sale;
        }

        public async Task<Sale> UpdateAsync(Sale sale)
        {
            await _saleRepository.UpdateAsync(sale);
            return sale;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sale = await _saleRepository.GetByIdWithItemsAsync(id);
            if (sale == null)
                return false;

            await _saleRepository.DeleteAsync(sale.Id);
            return true;
        }
    }
}
