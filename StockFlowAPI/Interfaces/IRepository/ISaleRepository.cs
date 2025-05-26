using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IRepository
{
    public interface ISaleRepository : IRepositoryBase<Sale>
    {
        Task<IEnumerable<Sale>> GetAllWithItemsAsync();
        Task<Sale?> GetByIdWithItemsAsync(int id);
    }
}
