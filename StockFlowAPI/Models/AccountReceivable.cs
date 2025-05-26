namespace StockFlowAPI.Models
{
    public class AccountReceivable
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsReceived { get; set; } = false;

        public Sale? Sale { get; set; }
    }
}
