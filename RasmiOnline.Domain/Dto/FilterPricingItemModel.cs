namespace RasmiOnline.Domain.Dto
{
    using RasmiOnline.Domain.Enum;
    using RasmiOnline.Domain.Properties;
    using System.ComponentModel.DataAnnotations;

    public class FilterPricingItemModel : FilterBaseModel
    {

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.PricingItemCategoryId))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public PricingItemCategory CategoryId { get; set; }

        [Display(Name = nameof(DisplayName.DocumentType), ResourceType = typeof(DisplayName))]
        public string DocumentType { get; set; }

        [Display(Name = nameof(DisplayName.Lable), ResourceType = typeof(DisplayName))]
        public string Lable { get; set; }

        [Display(Name = nameof(DisplayName.Unit), ResourceType = typeof(DisplayName))]
        public PricingItemUnit PricingItemUnit { get; set; }
    }
}