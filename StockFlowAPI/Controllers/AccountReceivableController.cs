using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Controllers
{
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
        public async Task<ActionResult<IEnumerable<AccountReceivable>>> GetAll()
        {
            var accounts = await _service.GetAllAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountReceivable>> GetById(int id)
        {
            var account = await _service.GetByIdAsync(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<AccountReceivable>> Create(AccountReceivable account)
        {
            var created = await _service.CreateAsync(account);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AccountReceivable account)
        {
            if (id != account.Id) return BadRequest("ID da URL difere do corpo");

            await _service.UpdateAsync(account);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/receive")]
        public async Task<IActionResult> MarkAsReceived(int id)
        {
            var account = await _service.GetByIdAsync(id);
            if (account == null) return NotFound();

            account.IsReceived = true;
            await _service.UpdateAsync(account);

            return NoContent();
        }
    }
}
