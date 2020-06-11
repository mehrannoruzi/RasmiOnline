namespace RasmiOnline.Domain.Enum
{
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Properties;

    public enum MessagingType
    {
        [LocalizeDescription(nameof(DisplayName.RoboTele), typeof(DisplayName))]
        RoboTele = 1,

        [LocalizeDescription(nameof(DisplayName.Email), typeof(DisplayName))]
        Email = 2,

        [LocalizeDescription(nameof(DisplayName.Sms), typeof(DisplayName))]
        Sms = 3,
    }
}
