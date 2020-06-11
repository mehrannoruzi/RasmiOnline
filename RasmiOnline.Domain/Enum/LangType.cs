namespace RasmiOnline.Domain.Enum
{
    using Gnu.Framework.Core;
    using Properties;

    public enum LangType : byte
    {
        [Visible]
        [LocalizeDescription(nameof(DisplayName.Fa_En), typeof(DisplayName))]
        Fa_En = 1,

        [Visible]
        [LocalizeDescription(nameof(DisplayName.Fa_Ru), typeof(DisplayName))]
        Fa_Ru = 2,

        [Visible]
        [LocalizeDescription(nameof(DisplayName.Fa_Ar), typeof(DisplayName))]
        Fa_Ar = 3,
    
        [LocalizeDescription(nameof(DisplayName.Fa_It), typeof(DisplayName))]
        Fa_It = 4,

        [Visible]
        [LocalizeDescription(nameof(DisplayName.Fa_Tr), typeof(DisplayName))]
        Fa_Tr = 5,
       
        [LocalizeDescription(nameof(DisplayName.Fa_Or), typeof(DisplayName))]
        Fa_Or = 6,

        [Visible]
        [LocalizeDescription(nameof(DisplayName.Fa_Gr), typeof(DisplayName))]
        Fa_Gr = 7,

        [Visible]
        [LocalizeDescription(nameof(DisplayName.Fa_Fr), typeof(DisplayName))]
        Fa_Fr = 8
    }
}