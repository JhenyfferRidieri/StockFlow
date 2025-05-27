namespace StockFlowAPI.Dtos
{
    public class FinancialReportDto
    {
        public decimal TotalSales { get; set; }
        public decimal TotalAccountsReceivablePending { get; set; }
        public decimal TotalAccountsReceivableReceived { get; set; }
        public decimal TotalAccountsPayablePending { get; set; }
        public decimal TotalAccountsPayablePaid { get; set; }
        public decimal CostOfMaterials { get; set; }
        public decimal GrossProfit { get; set; }
    }
}