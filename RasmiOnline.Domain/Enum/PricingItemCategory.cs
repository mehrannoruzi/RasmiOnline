namespace RasmiOnline.Domain.Enum
{
    using System.ComponentModel;

    public enum PricingItemCategory : byte
    {
        [Description("اسناد تحصیلی")]
        EducationalDocuments = 1,

        [Description("اسناد شخصی")]
        PersonalDocuments = 2,

        [Description("اسناد قراردادی")]
        ContractDocuments = 3,

        [Description("اسناد قضایی")]
        JudicialDocuments = 4,

        [Description("اسناد مالکیت")]
        OwnershipDocuments = 5,

        [Description("اسناد معاملاتی")]
        TradingDocuments = 6,

        [Description("مجوز")]
        License = 7,

        [Description("گواهی")]
        Certification = 8,

        [Description("مدارک شرکتی")]
        CorporateDocuments = 9,

        [Description("متفرقه")]
        Other = 10
    }
}
