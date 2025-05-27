namespace StockFlowAPI.Models
{
    public class AccountReceivable
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsReceived { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int SaleId { get; set; }  // Vincula com a Venda
        public Sale Sale { get; set; } = null!;
    }
}
