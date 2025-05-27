namespace StockFlowAPI.Dtos
{
    public class InventoryMovementRequestDto
    {
        public int MaterialId { get; set; }
        public required string Type { get; set; }  
        public int Quantity { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
