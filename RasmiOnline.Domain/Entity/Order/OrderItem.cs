namespace RasmiOnline.Domain.Entity
{
    using Enum;
    using Properties;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderItems", Schema = "Order")]
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        public int OrderId { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.IsPricingItem))]
        public bool IsPricingItem { get; set; }

        public int PricingItemId { get; set; }

        [Display(Name = nameof(DisplayName.Count), ResourceType = typeof(DisplayName))]
        public int Count { get; set; }

        [Display(Name = nameof(DisplayName.Copy), ResourceType = typeof(DisplayName))]
        public int Copy { get; set; }


        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.CopyPrice))]
        public int CopyPrice { get; set; }


        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.CopyPrice_OthersLang))]
        public int CopyPrice_OthersLang { get; set; }

        [Display(Name = nameof(DisplayName.Price), ResourceType = typeof(DisplayName))]
        public int Price { get; set; }

        [Display(Name = nameof(DisplayName.Price_OthersLang), ResourceType = typeof(DisplayName))]
        public int Price_OthersLang { get; set; }

        [Display(Name = nameof(DisplayName.OrderItemType), ResourceType = typeof(DisplayName))]
        public OrderItemType OrderItemType { get; set; }

        [Display(Name = nameof(DisplayName.Unit), ResourceType = typeof(DisplayName))]
        public string PricingItemUnitText { get; set; }

        [Column(TypeName = "nvarchar")]
        [Display(Name = nameof(DisplayName.DocumentType), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Name { get; set; }

        [NotMapped]
        [Display(Name = nameof(DisplayName.Warning), ResourceType = typeof(DisplayName))]
        public string Description { get; set; }

        public int CalculateTotalCopyPrice(LangType langType) => Count * Copy * (langType == LangType.Fa_En ? CopyPrice : CopyPrice_OthersLang);

        public int CalculateTotalPrice(LangType langType)
        {
            switch (OrderItemType)
            {
                case OrderItemType.OfficialRecordItem:
                    return (Count + Copy) * Price;
                case OrderItemType.DiscountItem:
                    return Price;
                default://PricingItems
                    return Count * (langType == LangType.Fa_En ? Price : Price_OthersLang) + CalculateTotalCopyPrice(langType);
            }
        }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
