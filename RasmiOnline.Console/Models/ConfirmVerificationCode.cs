namespace RasmiOnline.Console
{
    using RasmiOnline.Domain.Properties;
    using System.ComponentModel.DataAnnotations;
    public class ConfirmVerificationCode
    {
        [Display(Name = nameof(DisplayName.Code), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(6, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(6,MinimumLength =6, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Code{ get; set; }
    }
}