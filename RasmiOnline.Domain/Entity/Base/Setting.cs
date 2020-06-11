namespace RasmiOnline.Domain.Entity
{
    using Properties;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(Setting), Schema = "Base")]
    public class Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettingId { get; set; }

        [Display(Name = nameof(DisplayName.DayToDelivery), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public byte DayToDelivery { get; set; }

        [Display(Name = nameof(DisplayName.LimitOrderOpenDay), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int LimitOrderOpenDay { get; set; }

        [Display(Name = nameof(DisplayName.Warning), ResourceType = typeof(DisplayName))]
        [MaxLength(200, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ImportantNotice { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.Tel), ResourceType = typeof(DisplayName))]
        [MaxLength(35, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(35, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Tel { get; set; }

        [Display(Name = nameof(DisplayName.IsConst), ResourceType = typeof(DisplayName))]
        public int OfficialRecordPrice { get; set; }
    }
}
