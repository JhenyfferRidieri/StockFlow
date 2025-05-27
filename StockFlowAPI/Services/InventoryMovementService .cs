using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
    public class InventoryMovementService : IInventoryMovementService
    {
        private readonly IInventoryMovementRepository _movementRepository;

        public InventoryMovementService(IInventoryMovementRepository movementRepository)
        {
            _movementRepository = movementRepository;
        }

        public async Task<IEnumerable<InventoryMovement>> GetAllAsync()
        {
            return await _movementRepository.GetAllWithMaterialAsync();
        }

        public async Task<InventoryMovement?> GetByIdAsync(int id)
        {
            return await _movementRepository.GetByIdWithMaterialAsync(id);
        }

        public async Task<IEnumerable<InventoryMovement>> GetByMaterialIdAsync(int materialId)
        {
            return await _movementRepository.GetByMaterialIdAsync(materialId);
        }

        public async Task<InventoryMovement> CreateAsync(InventoryMovement movement)
        {
            return await _movementRepository.AddAsync(movement);
        }
    }
}
