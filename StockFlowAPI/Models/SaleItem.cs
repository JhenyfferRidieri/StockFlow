namespace StockFlowAPI.Models
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int MaterialId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Sale Sale { get; set; } = null!;
        public Material Material { get; set; } = null!;
    }

}