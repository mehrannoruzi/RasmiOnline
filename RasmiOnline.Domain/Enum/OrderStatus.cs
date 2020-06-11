namespace RasmiOnline.Domain.Enum
{
    using Properties;
    using Gnu.Framework.Core;

    public enum OrderStatus : byte
    {
        [LocalizeDescription(nameof(DisplayName.WaitForPricing), typeof(DisplayName))]
        WaitForPricing = 4,

        [LocalizeDescription(nameof(DisplayName.WaitForPayment), typeof(DisplayName))]
        WaitForPayment = 6,

        [LocalizeDescription(nameof(DisplayName.WaitForDeliverDocumentByUser), typeof(DisplayName))]
        WaitForDeliverDocumentByUser = 8,

        [LocalizeDescription(nameof(DisplayName.WaitForDeliverDocumentByPeyk), typeof(DisplayName))]
        WaitForDeliverDocumentByPeyk = 10,

        [LocalizeDescription(nameof(DisplayName.WaitForDeliverDocumentByPost), typeof(DisplayName))]
        WaitForDeliverDocumentByPost = 12,

        [LocalizeDescription(nameof(DisplayName.UploadFiles), typeof(DisplayName))]
        UploadFiles = 13,

        [LocalizeDescription(nameof(DisplayName.StartTranslation), typeof(DisplayName))]
        StartTranslation = 14,

        [LocalizeDescription(nameof(DisplayName.SubmitDraft), typeof(DisplayName))]
        SubmitDraft = 16,

        [LocalizeDescription(nameof(DisplayName.PayAllFactor), typeof(DisplayName))]
        PayAllFactor = 18,

        [LocalizeDescription(nameof(DisplayName.DeliveryFiles), typeof(DisplayName))]
        DeliveryFiles = 20,

        [LocalizeDescription(nameof(DisplayName.Done), typeof(DisplayName))]
        Done = 22,

        [LocalizeDescription(nameof(DisplayName.CancledOrder), typeof(DisplayName))]
        Cancel = 100
    }
}
