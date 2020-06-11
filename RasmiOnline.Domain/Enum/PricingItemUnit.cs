namespace RasmiOnline.Domain.Enum
{
    using System.ComponentModel;

    public enum PricingItemUnit : byte
    {
        [Description("سطر")]
        Row = 1,

        [Description("صفحه")]
        Page = 2,

        [Description("ترم")]
        Term = 3,

        [Description("سال")]
        Year = 4
    }
}