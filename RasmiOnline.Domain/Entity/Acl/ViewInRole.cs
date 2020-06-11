namespace RasmiOnline.Domain.Entity
{
    using System;
    using Properties;
    using Gnu.Framework.EntityFramework;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(ViewInRole), Schema = "Acl")]
    public class ViewInRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ViewInRoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.Role))]
        public int RoleId { get; set; }

        [ForeignKey(nameof(ViewId))]
        public View View { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.View))]
        public int ViewId { get; set; }

        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.IsDefault))]
        public bool IsDefault { get; set; }

        public DateTime ExpireDateMi { get; set; }

        [Column(TypeName = "char")]
        [Display(ResourceType = typeof(DisplayName), Name = nameof(DisplayName.ExpireDate))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [PersianDate(ErrorMessageResourceType = typeof(ErrorMessage), ErrorMessageResourceName = nameof(ErrorMessage.PersianDate))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ExpireDateSh { get; set; }
        
    }
}
