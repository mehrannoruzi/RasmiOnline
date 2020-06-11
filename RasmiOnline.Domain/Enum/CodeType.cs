namespace RasmiOnline.Domain.Enum
{
    using System.ComponentModel;
    public enum CodeType : byte
    {
        [Description("فقط عدد")]
        Digit = 1,

        [Description("فقط حروف")]
        Character = 2,

        [Description("هر دو")]
        Both = 3
    }
}