namespace RasmiOnline.Domain.Dto
{
    using RasmiOnline.Domain.Properties;
    using System.ComponentModel.DataAnnotations;

    public class FilterUserModel : FilterBaseModel
    {
        [Display(Name = nameof(DisplayName.MobileNumber), ResourceType = typeof(DisplayName))]
        public long? MobileNumber { get; set; }

        [Display(Name = nameof(DisplayName.IsActive), ResourceType = typeof(DisplayName))]
        public bool? IsActive { get; set; }

        [Display(Name = nameof(DisplayName.IsBlock), ResourceType = typeof(DisplayName))]
        public bool? IsBlock { get; set; }

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
    }
}