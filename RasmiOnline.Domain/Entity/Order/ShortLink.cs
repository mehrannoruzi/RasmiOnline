namespace RasmiOnline.Domain.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(ShortLink), Schema = "Order")]
    public class ShortLink
    {
        [Key]
        public int ShortLinkId { get; set; }
        public int OrderId { get; set; }
        public Guid UserId { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "varchar")]
        public string Code { get; set; }
    }
}