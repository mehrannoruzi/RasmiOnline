namespace RasmiOnline.Domain.Enum
{
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Properties;

    public enum EntityName:byte
    {
        [LocalizeDescription(nameof(DisplayName.Users), typeof(DisplayName))]
        User = 1,

        [LocalizeDescription(nameof(DisplayName.Orders), typeof(DisplayName))]
        Order = 2
    }
}
