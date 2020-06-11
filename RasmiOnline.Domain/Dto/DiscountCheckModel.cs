
namespace RasmiOnline.Domain.Dto
{
    using Domain.Enum;
    using System;
    public class DiscountCheckModel
    {
        public string Code { set; get; }
        public int TotalPrice { set; get; }
        public Guid UserId { set; get; }
        public LangType LangType { set; get; }
        public int OrderId { get; set; }

    }
}
