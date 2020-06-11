namespace RasmiOnline.Domain.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DiscountOrders", Schema = "Order")]
    public class DiscountOrder
    {
        [Key]
        public int DiscountOrderId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        [Required]
        public int DiscountId { get; set; }

        [ForeignKey(nameof(DiscountId))]
        public Discount Discount { get; set; }


    }
}
