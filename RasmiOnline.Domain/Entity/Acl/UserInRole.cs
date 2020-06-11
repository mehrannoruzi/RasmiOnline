namespace RasmiOnline.Domain.Entity
{
    using System;
    using Properties;
    using Gnu.Framework.EntityFramework;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(UserInRole), Schema = "Acl")]
    public class UserInRole  
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserInRoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.Role))]
        public int RoleId { get; set; }

        [Display(Name = nameof(DisplayName.IsActive), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsActive { get; set; }

        public DateTime ExpireDateMi { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.ExpireDate))]
        [Column(TypeName = "char")]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [PersianDate(ErrorMessageResourceType = typeof(ErrorMessage), ErrorMessageResourceName = nameof(ErrorMessage.PersianDate))]
        public string ExpireDateSh { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Display(Name = nameof(DisplayName.User), ResourceType = typeof(DisplayName))]
        public Guid UserId { get; set; }
    }
}
