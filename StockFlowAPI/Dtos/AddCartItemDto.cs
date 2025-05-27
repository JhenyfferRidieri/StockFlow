namespace StockFlowAPI.Dtos
{
    public class AddCartItemDto
    {
        public int SaleId { get; set; } 
        public int MaterialId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } 
    }

    public class UpdateCartItemDto
    {
        public int SaleItemId { get; set; }
        public int Quantity { get; set; }
    }
}
