namespace StockFlowAPI.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public Material Material { get; set; } = null!;
    }

}

