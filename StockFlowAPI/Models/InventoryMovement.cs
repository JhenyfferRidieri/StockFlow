namespace StockFlowAPI.Models
{
	public class InventoryMovement
	{
		public int Id { get; set; }
		public int MaterialId { get; set; }
		public string Type { get; set; } = null!; 
		public int Quantity { get; set; }
		public string Description { get; set; } = null!;
		public DateTime Date { get; set; } = DateTime.Now;

		public Material Material { get; set; } = null!;
	}
}
