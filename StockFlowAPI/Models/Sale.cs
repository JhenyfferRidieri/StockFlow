namespace StockFlowAPI.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string Status { get; set; } = string.Empty;
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
