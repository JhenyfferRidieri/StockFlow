using StockFlowAPI.Models.Enum;

namespace StockFlowAPI.Models
{
    public class AccountPayable
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public CostType CostType { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
