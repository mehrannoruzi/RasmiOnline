namespace RasmiOnline.Domain.Enum
{
    using System.ComponentModel;

    public enum PaymentGatewayType : byte
    {
        [Description("درگاه بانکی")]
        Online = 1,

        [Description("کارت خوان")]
        Pos = 2,

        [Description("کارت به کارت")]
        Transfer = 4
    }
}
