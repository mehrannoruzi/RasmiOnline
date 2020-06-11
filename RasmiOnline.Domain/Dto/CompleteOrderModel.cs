namespace RasmiOnline.Domain.Dto
{
    using Enum;
    using System;
    public class CompleteOrderModel
    {
        public Guid UserId { get; set; }

        public int OrderId { get; set; }

        public string DiscountCode { get; set; }

        public int? AddressId { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public PaymentType PaymentType { get; set; }

        public int PaymentGatewayId { get; set; }
    }
}
