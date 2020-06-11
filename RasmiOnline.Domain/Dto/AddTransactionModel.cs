namespace RasmiOnline.Domain.Dto
{
    public class AddTransactionModel
    {
        public int PaymentGatewayId { get; set; }
        public int OrderId { get; set; }
        public int Price { get; set; }
        public bool IsSuccess { get; set; }
        public string InsertDateSh { get; set; }
        public string TrackingId { get; set; }

    }
}