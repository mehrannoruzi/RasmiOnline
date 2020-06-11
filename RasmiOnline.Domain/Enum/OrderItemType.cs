
namespace RasmiOnline.Domain.Enum
{
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Properties;

    public enum OrderItemType
    {
        //[LocalizeDescription(nameof(DisplayName.PricingItem), typeof(DisplayName))]
        PricingItem,
        //[LocalizeDescription(nameof(DisplayName.DiscountItem), typeof(DisplayName))]
        DiscountItem,
        LicenseItem,
        DeliveryItem,
        OfficialRecordItem,
        //new ones

    }
}
