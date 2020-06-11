namespace RasmiOnline.Domain.Dto
{
    using System;
    using RasmiOnline.Domain.Enum;
    using RasmiOnline.Domain.Properties;
    using System.ComponentModel.DataAnnotations;

    public class OrderDetailsModel
    {
        public int OrderId { get; set; }

        [Display(Name = nameof(DisplayName.OrderNumber), ResourceType = typeof(DisplayName))]
        public int OrderNumber { get; set; }

        [Display(Name = nameof(DisplayName.OrderStatus), ResourceType = typeof(DisplayName))]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = nameof(DisplayName.PaymentDeliveryType), ResourceType = typeof(DisplayName))]
        public DeliveryType DeliverType { get; set; }//true = by myself, false = post it

        [Display(Name = nameof(DisplayName.InsertDateMi), ResourceType = typeof(DisplayName))]
        public DateTime InsertDateMi { get; set; }

        [Display(Name = nameof(DisplayName.InsertDateSh), ResourceType = typeof(DisplayName))]
        public string InsertDateSh { get; set; }

        [Display(Name = nameof(DisplayName.FullName), ResourceType = typeof(DisplayName))]
        public string UserFullName { get; set; }

        [Display(Name = nameof(DisplayName.MobileNumber), ResourceType = typeof(DisplayName))]
        public long MobileNumber { get; set; }

        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string OrderTitle { get; set; }

    }
}
