namespace RasmiOnline.Console
{
    using RasmiOnline.Domain.Properties;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SignInModel
    {
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMessage.Email), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Display(Name = nameof(DisplayName.Email), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage), AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [MaxLength(15, ErrorMessageResourceName = nameof(ErrorMessage.Min5MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MinLength(5, ErrorMessageResourceName = nameof(ErrorMessage.MinLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(15, MinimumLength = 5, ErrorMessageResourceName = nameof(ErrorMessage.Min5MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Display(Name = nameof(Password), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage), AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Display(Name = nameof(RememberMe), ResourceType = typeof(DisplayName))]
        public bool RememberMe { get; set; }
    }
}
