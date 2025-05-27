using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(bool? onlyActive = null)
        {
            var all = await _repository.GetAllAsync();

            if (onlyActive.HasValue)
                return all.Where(e => e.IsActive == onlyActive.Value);

            return all;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            employee.IsActive = true;
            await _repository.AddAsync(employee);
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            await _repository.UpdateAsync(employee);
            return employee;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return false;

            employee.IsActive = false;
            await _repository.UpdateAsync(employee);
            return true;
        }
    }
}
