namespace RasmiOnline.Domain.Entity
{
    using System;
    using Properties;
    using System.Collections.Generic;
    using Gnu.Framework.EntityFramework;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(View), Schema = "Acl")]
    public class View
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ViewId { get; set; }

        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Display(Name = nameof(DisplayName.ParentId), ResourceType = typeof(DisplayName))]
        public int ParentId { get; set; }

        [Display(Name = nameof(DisplayName.OrderPriority), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public byte OrderPriority { get; set; }

        [Display(Name = nameof(DisplayName.IsVisible), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsVisible { get; set; }

        [Display(Name = nameof(DisplayName.ExpireDate), ResourceType = typeof(DisplayName))]
        public DateTime ExpireDateMi { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(DisplayName.ExpireDate), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [PersianDate(ErrorMessageResourceType = typeof(ErrorMessage), ErrorMessageResourceName = nameof(ErrorMessage.PersianDate))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ExpireDateSh { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.Controller), ResourceType = typeof(DisplayName))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Controller { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.ActionName), ResourceType = typeof(DisplayName))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ActionName { get; set; }

        [Display(Name = nameof(DisplayName.ActionNameFa), ResourceType = typeof(DisplayName))]
        [MaxLength(35, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(35, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ActionNameFa { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.Icon), ResourceType = typeof(DisplayName))]
        [MaxLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Icon { get; set; }

        [NotMapped]
        public int RoleId { get; set; }

        [NotMapped]
        public bool IsDefault { get; set; }

        public virtual ICollection<ViewInRole> ViewInRoles { get; set; }
    }
}
