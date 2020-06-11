namespace RasmiOnline.Domain.Entity
{
    using Enum;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PricingItemTemplate", Schema = "Base")]
    public class PricingItemTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PricingItemTemplateId { get; set; }

        public int PricingItemId { get; set; }

        [ForeignKey(nameof(PricingItemId))]
        public PricingItem PricingItem { get; set; }

        public  CalculatorTemplate CalculatorTemplate { get; set; }
    }
}
