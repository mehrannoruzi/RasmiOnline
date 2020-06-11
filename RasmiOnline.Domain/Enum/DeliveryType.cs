namespace RasmiOnline.Domain.Enum
{
    using Gnu.Framework.Core;
    using Properties;

    public enum DeliveryType : byte
    {

        [LocalizeDescription(nameof(DisplayName.ByDeliveryMan), typeof(DisplayName))]
        ByDeliveryMan = 0,

        [LocalizeDescription(nameof(DisplayName.DeliverNyMySelf), typeof(DisplayName))]
        ByMySelf = 1,

        [LocalizeDescription(nameof(DisplayName.ByPost), typeof(DisplayName))]
        ByPost = 2
    }
}
