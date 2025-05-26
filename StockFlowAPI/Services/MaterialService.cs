using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
        {
            return await _materialRepository.GetAllOrderedAsync();
        }

        public async Task<Material?> GetByIdAsync(int id)
        {
            return await _materialRepository.GetByIdAsync(id);
        }

        public async Task<Material?> GetByNameAsync(string name)
        {
            return await _materialRepository.GetByNameAsync(name);
        }

        public async Task<Material> CreateAsync(Material material)
        {
            await _materialRepository.AddAsync(material);
            return material;
        }

        public async Task<Material> UpdateAsync(Material material)
        {
            await _materialRepository.UpdateAsync(material);
            return material;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var material = await _materialRepository.GetByIdAsync(id);

            if (material == null)
                return false;

            await _materialRepository.DeleteAsync(material.Id);
            return true;
        }
    }
}