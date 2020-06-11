namespace RasmiOnline.Domain.Enum
{
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Properties;
    public enum  PaymentType: byte
    {
        [LocalizeDescription(nameof(DisplayName.ByGateway), typeof(DisplayName))]
        ByGateway = 0,

        [LocalizeDescription(nameof(DisplayName.InPerson), typeof(DisplayName))]
        InPerson = 1
    }
}
