namespace StockFlowAPI.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }

}
