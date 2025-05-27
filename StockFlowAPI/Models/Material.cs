namespace StockFlowAPI.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string? Supplier { get; set; }
        public string Size { get; set; } = null!;
        public string Color { get; set; } = null!;

        
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
