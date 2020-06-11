namespace RasmiOnline.Domain.Enum
{
    using System.ComponentModel;

    public enum CalculatorTemplate : byte
    {
        [Description("ثبت طلاق در خارج از کشور")]
        MarriageAndDivorce = 1,

        [Description("ثبت ازدواج در خارج از کشور")]
        Marriage = 2,

        [Description("ثبت شرکت ایرانی در خارج از کشور")]
        Company = 3,

        [Description("انجام امور حقوقی شرکت های خارجی")]
        Balance = 4,

        [Description("اخذ ویزای مهاجرت به کشورهای مختلف")]
        Migration = 5,

        [Description("اخذ ویزای توریستی")]
        Tourist = 6,

        [Description("اخذ پذیرش از دانشگاه های خارج از کشور")]
        Graduate = 7,
    }
}
