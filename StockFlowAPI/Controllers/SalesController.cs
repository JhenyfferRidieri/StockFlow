using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;
using StockFlowAPI.Models.Enum;
using StockFlowAPI.Dto;

namespace StockFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // GET: api/sales
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _saleService.GetAllAsync();
            return Ok(sales);
        }

        // GET: api/sales/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null) return NotFound();
            return Ok(sale);
        }

        // POST: api/sales
        [HttpPost]
        public async Task<IActionResult> Create(Sale sale)
        {
            var created = await _saleService.CreateAsync(sale);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/sales/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Sale sale)
        {
            if (id != sale.Id) return BadRequest();

            var updated = await _saleService.UpdateAsync(sale);
            return Ok(updated);
        }

        // DELETE: api/sales/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _saleService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // PATCH: api/sales/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateSaleStatusDto dto)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null) return NotFound();

            sale.Status = Enum.Parse<SaleStatus>(dto.Status, ignoreCase: true);
            await _saleService.UpdateAsync(sale);

            return Ok(sale);
        }

        // POST: api/sales/{id}/pay
        [HttpPost("{id}/pay")]
        public async Task<IActionResult> PaySale(int id)
        {
            try
            {
                var success = await _saleService.PaySaleAsync(id);

                if (!success)
                    return BadRequest("Falha no pagamento. Estoque insuficiente ou venda não encontrada.");

                return Ok("Pagamento realizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/sales/{id}/cancel
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelSale(int id, [FromBody] CancelSaleDto dto)
        {
            try
            {
                var success = await _saleService.CancelSaleAsync(id, dto.Reason);

                if (!success)
                    return BadRequest("Falha ao cancelar a venda.");

                return Ok("Venda cancelada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}