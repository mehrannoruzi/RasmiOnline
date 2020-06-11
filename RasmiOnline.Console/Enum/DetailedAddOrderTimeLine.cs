namespace RasmiOnline.Console.Enum
{
    using Gnu.Framework.Core;
    using Properties;
    public enum DetailedAddOrderTimeLine
    {
        [LocalizeDescription(nameof(LocalMessage.OrderBegin), typeof(LocalMessage))]
        OrderBegin,

        //[LocalizeDescription(nameof(LocalMessage.OrderPurpose), typeof(LocalMessage))]
        //OrderPurpose,

        [LocalizeDescription(nameof(LocalMessage.SelectOrderItem), typeof(LocalMessage))]
        SelectOrderItem,

        //[LocalizeDescription(nameof(LocalMessage.Done), typeof(LocalMessage))]
        //OrderSaved,

        [LocalizeDescription(nameof(LocalMessage.Pricing), typeof(LocalMessage))]
        Pricing,

        [LocalizeDescription(nameof(LocalMessage.ConfirmFactor), typeof(LocalMessage))]
        ConfirmFactor,

        //[LocalizeDescription(nameof(LocalMessage.UseDiscount), typeof(LocalMessage))]
        //UseDiscount,
        [LocalizeDescription(nameof(LocalMessage.SelectAddress), typeof(LocalMessage))]
        SelectAddress,

        [LocalizeDescription(nameof(LocalMessage.PaymentAllFactor), typeof(LocalMessage))]
        PaymentAllFactor,

        [LocalizeDescription(nameof(LocalMessage.UploadFiles), typeof(LocalMessage))]
        UploadFiles,

        [LocalizeDescription(nameof(LocalMessage.ConfirmDraft), typeof(LocalMessage))]
        ConfirmDraft,

        [LocalizeDescription(nameof(LocalMessage.Done), typeof(LocalMessage))]
        Done
    }
}