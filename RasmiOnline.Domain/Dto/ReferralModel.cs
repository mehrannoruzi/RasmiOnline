namespace RasmiOnline.Domain.Dto
{
    using Enum;
    public class ReferralModel
    {
        public long MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ReferralCode { get; set; }
        public string Email { get; set; }
        public int OrderId { get; set; }
        public int Percent { get; set; }
        public LangType LangType { get; set; }
        public int TotalPrice { get; set; }
        public int TotalPriceOtherLang { get; set; }
        public OrderStatus OrderStatus { get; set; }
     

        public string GetTotalPrice()
        {
            if (LangType == LangType.Fa_En)
                return TotalPrice.ToString("0,0");
            else
                return TotalPriceOtherLang.ToString("0,0");
        }

        public string GetReferralTotalPrice()
        {
            if (OrderStatus != OrderStatus.Done)
                return "0";
            if (LangType == LangType.Fa_En)
                return ((TotalPrice * Percent) / 100).ToString("0,0");
            else
                return ((TotalPriceOtherLang * Percent) / 100).ToString("0,0");
        }
    }
}