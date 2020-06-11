namespace RasmiOnline.Domain.Dto
{
    using System;
    using Domain.Properties;
    using System.ComponentModel.DataAnnotations;

    public class ChangePassword
    {
        [Required]
        public Guid U { get; set; }

        [Required]
        public string C { get; set; }

        [Display(Name = nameof(DisplayName.NewPassword), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MinLength(6, ErrorMessageResourceName = nameof(ErrorMessage.MinLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = nameof(ErrorMessage.Min6MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        //[RegularExpression("^(((.*[A-Z])(.*[a-z])(.*\\d))|((.*[A-Z])(.*\\d)(.*[a-z]))|((.*[a-z])(.*[A-Z])(.*\\d))|((.*[a-z])(.*\\d)(.*[A-Z]))|((.*\\d)(.*[A-Z])(.*[a-z]))|((.*\\d)(.*[a-z])(.*[A-Z]))).*$", ErrorMessageResourceName = nameof(ErrorMessage.PasswordComplexity), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessageResourceName = nameof(ErrorMessage.PasswordsNotMatched), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Display(Name = nameof(DisplayName.ConfirmPassword), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeUserPassword : ChangePassword
    {
        public Guid UserId { get; set; }
    }

    public class ChangeCurrentPassword : ChangePassword
    {
        [Display(Name = nameof(DisplayName.CurrentPassword), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MinLength(6, ErrorMessageResourceName = nameof(ErrorMessage.MinLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = nameof(ErrorMessage.Min6MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        //[RegularExpression("^(((.*[A-Z])(.*[a-z])(.*\\d))|((.*[A-Z])(.*\\d)(.*[a-z]))|((.*[a-z])(.*[A-Z])(.*\\d))|((.*[a-z])(.*\\d)(.*[A-Z]))|((.*\\d)(.*[A-Z])(.*[a-z]))|((.*\\d)(.*[a-z])(.*[A-Z]))).*$", ErrorMessageResourceName = nameof(ErrorMessage.PasswordComplexity), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string CurrentPassword { get; set; }
    }

}