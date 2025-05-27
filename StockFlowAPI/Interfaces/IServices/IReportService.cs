using StockFlowAPI.Dtos; 

namespace StockFlowAPI.Interfaces.IServices
{
    public interface IReportService
    {
        Task<FinancialReportDto> GetFinancialReportAsync(DateTime? startDate, DateTime? endDate);
    }
}
