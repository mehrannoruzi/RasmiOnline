namespace RasmiOnline.Domain.Dto
{
    using RasmiOnline.Domain.Enum;
    using RasmiOnline.Domain.Properties;
    using System.ComponentModel.DataAnnotations;

    public class FilterOrderModel:FilterBaseModel
    {
        [Display(Name = nameof(DisplayName.OrderNumber), ResourceType = typeof(DisplayName))]
        public int? OrderNumber { get; set; }

        [Display(Name = nameof(DisplayName.OrderStatus), ResourceType = typeof(DisplayName))]
        public OrderStatus? OrderStatus { get; set; }

        [Display(Name = nameof(DisplayName.PaymentDeliveryType), ResourceType = typeof(DisplayName))]
        public DeliveryType? DeliverType { get; set; }//true = by myself, false = post it

        [Display(Name = nameof(DisplayName.NationalCode), ResourceType = typeof(DisplayName))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string NationalCode { get; set; }

        [Display(Name = nameof(DisplayName.FirstName), ResourceType = typeof(DisplayName))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string FirstName { get; set; }

        [Display(Name = nameof(DisplayName.LastName), ResourceType = typeof(DisplayName))]
        [MaxLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string LastName { get; set; }

        [Display(Name = nameof(DisplayName.Email), ResourceType = typeof(DisplayName))]
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMessage.Email), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Email { get; set; }
    }
}