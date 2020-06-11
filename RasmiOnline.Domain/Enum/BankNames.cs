namespace RasmiOnline.Domain.Enum
{
    using System.ComponentModel;

    public enum BankNames : byte
    {
        [Description("زرین پال")]
        ZarinPal = 2,

        [Description("پی")]
        Pay = 4,

        [Description("بانک سپه")]
        Sepah = 1,

        [Description("بانک ملی")]
        Melli = 27,

        [Description("بانک ملت")]
        Mellat = 3,

        [Description("بانک دی")]
        Dey = 28,

        [Description("بانک شهر")]
        Shahr = 5,

        [Description("بانک توسعه تعاون")]
        Tose_eTaavon = 6,

        [Description("بانک ایران زمین")]
        IranZamin = 7,

        [Description("بانک کشاورزی")]
        Keshavarzi = 8,

        [Description("بانک رفاه")]
        Refah = 9,

        [Description("بانک اقتصاد نوین")]
        EghtesadNovin = 10,

        [Description("بانک مهر اقتصاد")]
        MehreEghtesad = 11,

        [Description("بانک سرمایه")]
        Sarmayeh = 12,

        [Description("بانک آینده")]
        Ayandeh = 13,

        [Description("بانک کارآفرین")]
        Karafarin = 14,

        [Description("بانک پارسیان")]
        Parsian = 15,

        [Description("بانک گردشگری")]
        Gardeshgari = 16,

        [Description("بانک توسعه صادرات")]
        Tose_eSaderat = 17,

        [Description("بانک سینا")]
        Sina = 18,

        [Description("بانک صادرات")]
        Saderat = 19,

        [Description("بانک مسکن")]
        Maskan = 20,

        [Description("بانک پست بانک")]
        PostBank = 21,

        [Description("بانک انصار")]
        Ansar = 22,

        [Description("بانک سامان")]
        Saman = 23,

        [Description("بانک تجارت")]
        Tejarat = 24,

        [Description("بانک پاسارگاد")]
        Pasargad = 25,


        [Description("مهر ایران")]
        Mehre_Iran = 26,
    }
}
