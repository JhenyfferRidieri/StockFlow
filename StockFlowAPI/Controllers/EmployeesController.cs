using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace StockFlowAPI.Controllers
{
    [Authorize(Roles = "Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? onlyActive)
        {
            var employees = await _service.GetAllAsync(onlyActive);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            var created = await _service.CreateAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id) return BadRequest();

            var updated = await _service.UpdateAsync(employee);
            return Ok(updated);
        }

        // Desativar funcionário (em vez de excluir)
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var success = await _service.DeactivateAsync(id);
            return success ? Ok("Funcionário desativado.") : NotFound();
        }
    }
}
