using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Dtos;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _inventoryRepository.GetAllWithMaterialAsync();
        }

        public async Task<Inventory?> GetByIdAsync(int id)
        {
            return await _inventoryRepository.GetByIdWithMaterialAsync(id);
        }

        public async Task<Inventory?> GetByMaterialIdAsync(int materialId)
        {
            return await _inventoryRepository.GetByMaterialIdAsync(materialId);
        }

        public async Task<Inventory> CreateAsync(Inventory inventory)
        {
            await _inventoryRepository.AddAsync(inventory);
            return inventory;
        }

        public async Task<Inventory> UpdateAsync(Inventory inventory)
        {
            await _inventoryRepository.UpdateAsync(inventory);
            return inventory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inventory = await _inventoryRepository.GetByIdWithMaterialAsync(id);

            if (inventory == null)
                return false;

            await _inventoryRepository.DeleteAsync(inventory.Id);
            return true;
        }

        public async Task<bool> ApplyInventoryMovementAsync(InventoryMovementRequestDto movement)
        {
            var inventory = await _inventoryRepository.GetByMaterialIdAsync(movement.MaterialId);

            if (inventory == null)
                throw new ArgumentException("Material não encontrado no estoque.");

            if (movement.Type == "entry")
            {
                inventory.Quantity += movement.Quantity;
            }
            else if (movement.Type == "exit")
            {
                if (inventory.Quantity < movement.Quantity)
                    throw new ArgumentException("Estoque insuficiente para saída.");

                inventory.Quantity -= movement.Quantity;
            }
            else
            {
                throw new ArgumentException("Tipo de movimentação inválido.");
            }

            await _inventoryRepository.UpdateAsync(inventory);
            return true;
        }
    }
}
