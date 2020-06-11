namespace RasmiOnline.Domain.Entity
{
    using System;
    using Properties;
    using RasmiOnline.Domain.Enum;
    using Gnu.Framework.EntityFramework;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(Discount), Schema = "Base")]
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountId { get; set; }

        [Display(Name = nameof(DisplayName.CodeType), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public CodeType CodeType { get; set; }

        [Display(Name = nameof(DisplayName.DiscountType), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public DiscountType DiscountType { get; set; }

        [Display(Name = nameof(DisplayName.Value), ResourceType = typeof(DisplayName))]
        public int Value { get; set; }

        [Display(Name = nameof(DisplayName.Ceiling), ResourceType = typeof(DisplayName))]
        public int Ceiling { get; set; }

        [Display(Name = nameof(DisplayName.IsUsed), ResourceType = typeof(DisplayName))]
        public bool IsUsed { get; set; }

        /// <summary>
        /// تعداد مصرف
        /// </summary>
        [Display(Name = nameof(DisplayName.IsOneTime), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool UseForOnce { get; set; }

        /// <summary>
        /// تعداد کاربر
        /// </summary>
        [Display(Name = nameof(DisplayName.IsFirstTime), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool ForFirstUser { get; set; }

        [Display(Name = nameof(DisplayName.ValidFromDateSh), ResourceType = typeof(DisplayName))]
        public DateTime ValidFromDateMi { get; set; }

        [Display(Name = nameof(DisplayName.ValidToDateSh), ResourceType = typeof(DisplayName))]
        public DateTime ValidToDateMi { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(DisplayName.ValidFromDateSh), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [PersianDate(ErrorMessageResourceName = nameof(ErrorMessage.PersianDate), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ValidFromDateSh { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(DisplayName.ValidToDateSh), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [PersianDate(ErrorMessageResourceName = nameof(ErrorMessage.PersianDate), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ValidToDateSh { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.Code), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(32, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(32, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Code { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public User User { get; set; }

        public Guid UserId { get; set; }
    }
}