using StockFlowAPI.Models;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync(bool? onlyActive = null);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeactivateAsync(int id);  // Desativar funcionário
    }
}
