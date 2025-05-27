using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;
using StockFlowAPI.Models.Enum;

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
            ValidateMaterial(material);

            var existing = await _materialRepository.GetByNameAsync(material.Name);
            if (existing != null)
                throw new ArgumentException("Já existe um produto com esse nome.");

            return await _materialRepository.AddAsync(material);
        }

        public async Task<Material> UpdateAsync(Material material)
        {
            ValidateMaterial(material);

            var existing = await _materialRepository.GetByIdAsync(material.Id);
            if (existing == null)
                throw new ArgumentException("Material não encontrado.");

            // Verificar se está alterando o nome para um que já existe
            var other = await _materialRepository.GetByNameAsync(material.Name);
            if (other != null && other.Id != material.Id)
                throw new ArgumentException("Já existe um produto com esse nome.");

            return await _materialRepository.UpdateAsync(material);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var material = await _materialRepository.GetByIdAsync(id);
            if (material == null)
                return false;

            await _materialRepository.DeleteAsync(material.Id);
            return true;
        }

        private void ValidateMaterial(Material material)
        {
            if (material.Price < 0)
                throw new ArgumentException("Preço não pode ser negativo.");

            if (material.Quantity < 0)
                throw new ArgumentException("Quantidade não pode ser negativa.");

            if (!ProductConstants.Sizes.Contains(material.Size))
                throw new ArgumentException("Tamanho inválido.");

            if (!ProductConstants.Colors.Contains(material.Color))
                throw new ArgumentException("Cor inválida.");
        }
    }
}
