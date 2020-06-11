namespace RasmiOnline.Domain.Entity
{
    using Enum;
    using Properties;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(OfficeAddress), Schema = "Base")]
    public class OfficeAddress
    {
        [Key]
        public int OfficeAddressId { get; set; }

        [Display(Name = nameof(DisplayName.LangType), ResourceType = typeof(DisplayName))]
        public LangType LangType { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.Tel), ResourceType = typeof(DisplayName))]
        [MaxLength(14, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(14, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Tel { get; set; }

        [Display(Name = nameof(DisplayName.WorkHour), ResourceType = typeof(DisplayName))]
        [MaxLength(200, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(200, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string WorkHour { get; set; }

        [Display(Name = nameof(DisplayName.OfficeName), ResourceType = typeof(DisplayName))]
        [MaxLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string DeliveryName { get; set; }

        [Display(Name = nameof(DisplayName.Location), ResourceType = typeof(DisplayName))]
        [MaxLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Location { get; set; }

        [Display(Name = nameof(DisplayName.FullAddress), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(350, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(350, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string FullAddress { get; set; }

        [Display(Name = nameof(DisplayName.MobileNumber), ResourceType = typeof(DisplayName))]
        [Column(TypeName = "varchar")]
        [MaxLength(11, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(11, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string MobileNumber { get; set; }

        public Guid UserId { get; set; }

        [Display(Name = nameof(DisplayName.IsActive), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsActive { get; set; }
    }
}
