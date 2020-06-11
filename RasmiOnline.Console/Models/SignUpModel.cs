namespace RasmiOnline.Console
{
    using Domain.Properties;
    using System.ComponentModel.DataAnnotations;

    public class SignUpModel
    {
        [Display(Name = nameof(DisplayName.FirstName), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string FirstName { get; set; }

        [Display(Name = nameof(DisplayName.LastName), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string LastName { get; set; }

        [MaxLength(11, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MinLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MinLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(11, MinimumLength = 10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Display(Name = nameof(DisplayName.MobileNumber), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage), AllowEmptyStrings = false)]
        public string MobileNumber { get; set; }

        [Display(Name = nameof(DisplayName.Username), ResourceType = typeof(DisplayName))]
        [MaxLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MinLength(5, ErrorMessageResourceName = nameof(ErrorMessage.MinLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage), AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMessage.Email), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Email { get; set; }

        [Display(Name = nameof(DisplayName.Password), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MinLength(6, ErrorMessageResourceName = nameof(ErrorMessage.MinLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = nameof(ErrorMessage.Min6MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string SignUpPassword { get; set; }

        [Compare(nameof(SignUpPassword), ErrorMessageResourceName = nameof(ErrorMessage.PasswordsNotMatched), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Display(Name = nameof(DisplayName.ConfirmPassword), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ConfirmPassword { get; set; }

        [Display(Name = nameof(DisplayName.ReferralCode), ResourceType = typeof(DisplayName))]
        [MaxLength(32, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(32, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ReferralCode { get; set; }
    }
}