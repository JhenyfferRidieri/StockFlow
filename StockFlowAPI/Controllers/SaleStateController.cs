using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;

namespace StockFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleStateController : ControllerBase
    {
        private readonly ISaleStateService _saleStateService;

        public SaleStateController(ISaleStateService saleStateService)
        {
            _saleStateService = saleStateService;
        }

        //PUT: api/salestate/{saleId}?status=Enviado
        [HttpPut("{saleId}")]
        public async Task<IActionResult> UpdateStatus(int saleId, [FromQuery] string status)
        {
            try
            {
                var success = await _saleStateService.UpdateStatusAsync(saleId, status);

                if (!success) return NotFound("Venda não encontrada.");

                return Ok($"Status da venda {saleId} alterado para {status}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
