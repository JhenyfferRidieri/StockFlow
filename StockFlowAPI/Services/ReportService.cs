using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Data;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Dto;

namespace StockFlowAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FinancialSummaryDto> GetFinancialSummaryAsync(DateTime? startDate, DateTime? endDate)
        {
            var salesQuery = _context.Sales.AsQueryable();
            var expensesQuery = _context.AccountsPayable.AsQueryable();

            if (startDate.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.CreatedAt >= startDate.Value);
                expensesQuery = expensesQuery.Where(e => e.CreatedAt >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.CreatedAt <= endDate.Value);
                expensesQuery = expensesQuery.Where(e => e.CreatedAt <= endDate.Value);
            }

            var revenue = await salesQuery.SumAsync(s => (decimal?)s.Total) ?? 0;
            var expenses = await expensesQuery.SumAsync(e => (decimal?)e.Amount) ?? 0;

            return new FinancialSummaryDto
            {
                Revenue = revenue,
                Expense = expenses,
                Balance = revenue - expenses
            };
        }
    }
}
