using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Data;
using StockFlowAPI.Dtos;
using StockFlowAPI.Interfaces.IServices;

namespace StockFlowAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FinancialReportDto> GetFinancialReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var salesQuery = _context.Sales.AsQueryable();
            if (startDate.HasValue)
                salesQuery = salesQuery.Where(s => s.CreatedAt >= startDate.Value);
            if (endDate.HasValue)
                salesQuery = salesQuery.Where(s => s.CreatedAt <= endDate.Value);

            var totalSales = await salesQuery.SumAsync(s => s.Total);

            var receivables = await _context.AccountsReceivable.ToListAsync();
            var payables = await _context.AccountsPayable.ToListAsync();

            var totalReceivablePending = receivables.Where(r => !r.IsReceived).Sum(r => r.Amount);
            var totalReceivableReceived = receivables.Where(r => r.IsReceived).Sum(r => r.Amount);

            var totalPayablePending = payables.Where(p => !p.IsPaid).Sum(p => p.Amount);
            var totalPayablePaid = payables.Where(p => p.IsPaid).Sum(p => p.Amount);

            //  Custo dos materiais vendidos
            var saleItems = await _context.SaleItems
                .Include(i => i.Material)
                .Where(i => salesQuery.Select(s => s.Id).Contains(i.SaleId))
                .ToListAsync();

            var costOfMaterials = saleItems.Sum(i => i.Quantity * i.Material.Price);

            //  Lucro Bruto
            var grossProfit = totalSales - costOfMaterials - (totalPayablePaid + totalPayablePending);

            return new FinancialReportDto
            {
                TotalSales = totalSales,
                TotalAccountsReceivablePending = totalReceivablePending,
                TotalAccountsReceivableReceived = totalReceivableReceived,
                TotalAccountsPayablePending = totalPayablePending,
                TotalAccountsPayablePaid = totalPayablePaid,
                CostOfMaterials = costOfMaterials,
                GrossProfit = grossProfit
            };
        }
    }
}
