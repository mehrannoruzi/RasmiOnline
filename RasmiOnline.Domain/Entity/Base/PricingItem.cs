namespace RasmiOnline.Domain.Entity
{
    using Enum;
    using Properties;
    using Gnu.Framework.Core;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    [Table("PricingItems", Schema = "Base")]
    public class PricingItem : ISoftDeleteProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PricingItemId { get; set; }

        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Display(Name = nameof(DisplayName.Lable), ResourceType = typeof(DisplayName))]
        [MaxLength(200, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Lable { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.CopyPrice))]
        public int CopyPrice { get; set; }

        [Display(Name = nameof(DisplayName.Price), ResourceType = typeof(DisplayName))]
        public int Price { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.CopyPrice_OthersLang))]
        public int CopyPrice_OthersLang { get; set; }

        [Display(Name = nameof(DisplayName.Price_OthersLang), ResourceType = typeof(DisplayName))]
        public int Price_OthersLang { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.IsPricingItem))]
        public bool IsPricingItem { get; set; }

        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Display(Name = nameof(DisplayName.Unit), ResourceType = typeof(DisplayName))]
        public PricingItemUnit PricingItemUnit { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.PricingItemCategoryId))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public PricingItemCategory CategoryId { get; set; }

        [Display(Name = nameof(DisplayName.IsMustlyUse), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsMustlyUse { get; set; }

        [Display(Name = nameof(DisplayName.IsDiscountable), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsDiscountable { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "nvarchar")]
        [Display(Name = nameof(DisplayName.DocumentType), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string DocumentType { get; set; }

        [Display(Name = nameof(DisplayName.AboveReference), ResourceType = typeof(DisplayName))]
        [MaxLength(600, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string AboveReference { get; set; }

        [Display(Name = nameof(DisplayName.Warning), ResourceType = typeof(DisplayName))]
        [MaxLength(600, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Description { get; set; }

        [NotMapped]
        public string PricingItemUnitText { get { return PricingItemUnit > 0 ? PricingItemUnit.GetLocalizeDescription() : Enum.PricingItemUnit.Page.GetLocalizeDescription(); } }

        public ICollection<PricingItemTemplate> PricingItemTemplate { get; set; }
    }
}