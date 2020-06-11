using System;

namespace RasmiOnline.Domain.Dto
{
    public class TransactionModel
    {
        public Guid UserId;

        public int PaymentGatewayId { get; set; }
        public int OrderId { get; set; }
        public int Price { get; set; }
    }

    public class TransactionDetails
    {
        public int TransactionId { get; set; }
        public string PaymentGatewayName { get; set; }
        public string BankCard { get; set; }
        public int OrderId { get; set; }
        public int Price { get; set; }
        public bool IsSuccess { get; set; }
        public string InsertDateSh { get; set; }
        public string TrackingId { get; set; }
        public string Description { get; set; }
        public DateTime InsertDateMi { get; set; }
        public int  PaymentGatewayId { get; set; }
        public int BankCardId { get; set; } 
    }
}
