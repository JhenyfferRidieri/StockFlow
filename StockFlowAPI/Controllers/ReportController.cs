using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;

namespace StockFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("financial")]
        public async Task<IActionResult> GetFinancialReport(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            var report = await _reportService.GetFinancialReportAsync(startDate, endDate);
            return Ok(report);
        }
    }
}
