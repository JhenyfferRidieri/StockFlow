using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;
using StockFlowAPI.Models.Enum;
using StockFlowAPI.Dto;
using Microsoft.AspNetCore.Authorization;


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

        //  Funcionários - Listar vendas
        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _saleService.GetAllAsync();
            return Ok(sales);
        }

        // Funcionários - Buscar por ID
        [Authorize(Roles = "Employee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null) return NotFound();
            return Ok(sale);
        }

        //  Cliente - Criar venda
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(Sale sale)
        {
            var created = await _saleService.CreateAsync(sale);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Funcionários - Atualizar venda
        [Authorize(Roles = "Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Sale sale)
        {
            if (id != sale.Id) return BadRequest();

            var updated = await _saleService.UpdateAsync(sale);
            return Ok(updated);
        }

        // Funcionários - Deletar venda
        [Authorize(Roles = "Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _saleService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // Cliente - Pagar venda
        [AllowAnonymous]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateSaleStatusDto dto)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null) return NotFound();

            sale.Status = Enum.Parse<SaleStatus>(dto.Status, ignoreCase: true);
            await _saleService.UpdateAsync(sale);

            return Ok(sale);
        }

        // Cliente - Pagar venda
        [AllowAnonymous]
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

        //  Cliente - Cancelar venda
        [AllowAnonymous]
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