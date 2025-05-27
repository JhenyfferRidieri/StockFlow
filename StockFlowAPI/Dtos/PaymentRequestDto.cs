namespace StockFlowAPI.Dto
{
    public class PaymentRequestDto
    {
        public int SaleId { get; set; }
    }

    public class CancelSaleDto
    {
        public string Reason { get; set; } = string.Empty;
    }
}
