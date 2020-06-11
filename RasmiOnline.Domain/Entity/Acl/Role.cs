namespace RasmiOnline.Domain.Entity
{
    using Properties;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(Role), Schema = "Acl")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Display(Name = nameof(DisplayName.IsActive), ResourceType = typeof(DisplayName))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(DisplayName.IsDefault), ResourceType = typeof(DisplayName))]
        public bool IsDefault { get; set; }

        [Display(Name = nameof(DisplayName.RoleNameFa), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string RoleNameFa { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.RoleNameEn), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(30, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string RoleNameEn { get; set; }

        public virtual ICollection<ViewInRole> ViewInRoles { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
    }

}
