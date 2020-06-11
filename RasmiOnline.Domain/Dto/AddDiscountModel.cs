namespace RasmiOnline.Domain.Dto
{
    using System;
    using Enum;
    using Properties;
    using Gnu.Framework.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    public class AddDiscountModel
    {
        [Display(Name = nameof(DisplayName.IsOneTime), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool UseForOnce { get; set; }


        [Display(Name = nameof(DisplayName.IsFirstTime), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool ForFirstUser { get; set; }


        [Display(Name = nameof(DisplayName.DiscountType), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public DiscountType DiscountType { get; set; }


        [Display(Name = nameof(DisplayName.CodeType), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public CodeType CodeType { get; set; }


        [Display(Name = nameof(DisplayName.CodeLength), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int CodeLength { get; set; }


        [Display(Name = nameof(DisplayName.Count), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int Count { get; set; }


        [Display(Name = nameof(DisplayName.Value), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int Value { get; set; }


        [Display(Name = nameof(DisplayName.Ceiling), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int Ceiling { get; set; }


        [Display(Name = nameof(DisplayName.Code), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(32, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Code { get; set; }


        [Display(Name = nameof(DisplayName.ValidFromDateSh), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [PersianDate(ErrorMessageResourceName = nameof(ErrorMessage.PersianDate), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ValidFromDateSh { get; set; }


        [Display(Name = nameof(DisplayName.ValidToDateSh), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [PersianDate(ErrorMessageResourceName = nameof(ErrorMessage.PersianDate), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ValidToDateSh { get; set; }

        public Guid UserId { get; set; }
    }
}
