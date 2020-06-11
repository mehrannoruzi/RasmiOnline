namespace RasmiOnline.Domain.Enum
{
    using Gnu.Framework.Core;
    using Properties;

    public enum AttachmentType : byte
    {
        Draft,

        [LocalizeDescription(nameof(DisplayName.AttachmentTranslation), typeof(DisplayName))]
        Translation,

        [LocalizeDescription(nameof(DisplayName.AttachmentIdentity), typeof(DisplayName))]
        Identity,

        [LocalizeDescription(nameof(DisplayName.AttachmentOrderFiles), typeof(DisplayName))]
        OrderFiles
    }
}
