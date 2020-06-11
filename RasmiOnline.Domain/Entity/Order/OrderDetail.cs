namespace RasmiOnline.Domain.Entity
{
    using System;
    using Properties;
    using Gnu.Framework.Core;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderDetail", Schema = "Order")]
    public class OrderDetail : IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = nameof(DisplayName.OrderNumber), ResourceType = typeof(DisplayName))]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int PricingItemId { get; set; }

        [ForeignKey(nameof(OrderItemId))]
        public OrderItem OrderItem { get; set; }

        public int OrderItemId { get; set; }

        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(200, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Title { get; set; }

        [Display(Name = nameof(DisplayName.Count), ResourceType = typeof(DisplayName))]
        public int Count { get; set; }

        [Display(Name = nameof(DisplayName.Copy), ResourceType = typeof(DisplayName))]
        public int Copy { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.Description))]
        [MaxLength(600, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(600, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Description { get; set; }

        [Display(Name = nameof(DisplayName.InsertDateMi), ResourceType = typeof(DisplayName))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(DisplayName.InsertDateSh), ResourceType = typeof(DisplayName))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }
    }
}
