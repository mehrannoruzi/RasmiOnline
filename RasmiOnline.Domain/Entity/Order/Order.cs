namespace RasmiOnline.Domain.Entity
{
    using System;
    using Properties;
    using Domain.Enum;
    using Gnu.Framework.Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Gnu.Framework.EntityFramework;

    [Table("Orders", Schema = "Order")]
    public class Order : IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = nameof(DisplayName.OrderNumber), ResourceType = typeof(DisplayName))]
        public int OrderId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }
        public int? AddressId { get; set; }

        [Display(Name = nameof(DisplayName.OrderStatus), ResourceType = typeof(DisplayName))]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = nameof(DisplayName.DocsBeenRecieved), ResourceType = typeof(DisplayName))]
        public bool DocsBeenRecieved { get; set; }

        [Display(Name = nameof(DisplayName.LangType), ResourceType = typeof(DisplayName))]
        public LangType LangType { get; set; }

        [Display(Name = nameof(DisplayName.PaymentType), ResourceType = typeof(DisplayName))]
        public PaymentType PaymentType { get; set; }

        [Display(Name = nameof(DisplayName.IsConfirmedByClient), ResourceType = typeof(DisplayName))]
        public bool IsConfirmedByClient { get; set; }

        [Display(Name = nameof(DisplayName.DayToDelivery), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public byte DayToDelivery { get; set; }

        [Column(TypeName = "char")]
        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.DeliverFiles_DateSh))]
        [PersianDate(ErrorMessageResourceType = typeof(ErrorMessage), ErrorMessageResourceName = nameof(ErrorMessage.PersianDate))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string DeliverFiles_DateSh { get; set; }

        public DateTime? DeliverFiles_DateMi { get; set; }

        [Display(Name = nameof(DisplayName.PaymentDeliveryType), ResourceType = typeof(DisplayName))]
        public DeliveryType DeliveryType { get; set; } //true = by myself, false = post it

        [Display(Name = nameof(DisplayName.InsertDateMi), ResourceType = typeof(DisplayName))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(DisplayName.InsertDateSh), ResourceType = typeof(DisplayName))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public Guid UserId { get; set; }

        [Display(Name = nameof(DisplayName.OfficeUserId), ResourceType = typeof(DisplayName))]
        public Guid OfficeUserId { get; set; }

        [Display(Name = nameof(DisplayName.OrderTitle), ResourceType = typeof(DisplayName))]
        [MaxLength(70, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(70, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string OrderTitle { get; set; }

        [Display(Name = nameof(DisplayName.ConfirmComment), ResourceType = typeof(DisplayName))]
        [MaxLength(300, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(300, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ConfirmComment { get; set; }

        [Display(Name = nameof(DisplayName.ConfirmComment), ResourceType = typeof(DisplayName))]
        [MaxLength(500, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(500, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string OrderDescription { get; set; }

        [NotMapped]
        public int PayedValue { get { return Transactions.IsNull() ? 0 : Transactions.Where(x => x.IsSuccess).Sum(x => x.Price); } set { this.PayedValue = value; } }

        public int TotalPrice() => OrderItems.Sum(x => x.CalculateTotalPrice(LangType));

        public OrderStatus GetNextStatus()
        {
            if (OrderStatus == OrderStatus.PayAllFactor)
            {
                return OrderStatus.DeliveryFiles;
            }
            switch (DeliveryType)
            {
                case DeliveryType.ByMySelf:
                    return OrderStatus.WaitForDeliverDocumentByUser;
                case DeliveryType.ByDeliveryMan:
                    return OrderStatus.WaitForDeliverDocumentByPeyk;
                case DeliveryType.ByPost:
                    return OrderStatus.WaitForDeliverDocumentByPost;
            }
            return OrderStatus;
        }

        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<OrderName> OrderNames { get; set; }
        public ICollection<OrderComment> OrderComments { get; set; }

        [NotMapped]
        public string DeliveryName { get; set; }
    }
}
