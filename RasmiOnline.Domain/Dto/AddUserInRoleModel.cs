namespace RasmiOnline.Domain.Dto
{
    using Properties;
    using Gnu.Framework.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    public class AddUserInRoleModel
    {
        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.Role))]
        public int RoleId { get; set; }

        [Display(Name = nameof(DisplayName.IsActive), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.ExpireDate))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [PersianDate(ErrorMessageResourceType = typeof(ErrorMessage), ErrorMessageResourceName = nameof(ErrorMessage.PersianDate))]
        public string ExpireDateSh { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.MobileNumber))]
        public long SearchMobileNumber { get; set; }
    }
}