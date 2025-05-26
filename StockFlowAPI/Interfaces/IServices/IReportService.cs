using StockFlowAPI.Dto;

namespace StockFlowAPI.Interfaces.IServices
{
    public interface IReportService
    {
        Task<FinancialSummaryDto> GetFinancialSummaryAsync(DateTime? startDate, DateTime? endDate);
    }
}
