using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountPayableController : ControllerBase
    {
        private readonly IAccountPayableService _service;

        public AccountPayableController(IAccountPayableService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountPayable account)
        {
            var created = await _service.CreateAsync(account);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AccountPayable account)
        {
            if (id != account.Id) return BadRequest();

            var updated = await _service.UpdateAsync(account);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // Marcar como pago
        [HttpPut("{id}/pay")]
        public async Task<IActionResult> MarkAsPaid(int id)
        {
            var success = await _service.MarkAsPaidAsync(id);
            return success ? Ok("Conta marcada como paga.") : NotFound();
        }

        // Filtrar por status (pago, pendente, atrasado)
        [HttpGet("filter/status")]
        public async Task<IActionResult> FilterByStatus([FromQuery] string status)
        {
            var result = await _service.GetByStatusAsync(status);
            return Ok(result);
        }

        // Filtrar por tipo de custo
        [HttpGet("filter/costtype")]
        public async Task<IActionResult> FilterByCostType([FromQuery] string costType)
        {
            var result = await _service.GetByCostTypeAsync(costType);
            return Ok(result);
        }
    }
}
