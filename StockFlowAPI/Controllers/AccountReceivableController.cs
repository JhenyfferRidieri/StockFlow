using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace StockFlowAPI.Controllers
{
    [Authorize(Roles = "Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReceivableController : ControllerBase
    {
        private readonly IAccountReceivableService _service;

        public AccountReceivableController(IAccountReceivableService service)
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
        public async Task<IActionResult> Create([FromBody] AccountReceivable account)
        {
            var created = await _service.CreateAsync(account);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AccountReceivable account)
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

        // Marcar como Recebido
        [HttpPut("{id}/receive")]
        public async Task<IActionResult> MarkAsReceived(int id)
        {
            var success = await _service.MarkAsReceivedAsync(id);
            return success ? Ok("Conta marcada como recebida.") : NotFound();
        }

        // Filtrar por status (pago, pendente, vencido)
        [HttpGet("filter/status")]
        public async Task<IActionResult> FilterByStatus([FromQuery] string status)
        {
            var result = await _service.GetByStatusAsync(status);
            return Ok(result);
        }
    }
}
