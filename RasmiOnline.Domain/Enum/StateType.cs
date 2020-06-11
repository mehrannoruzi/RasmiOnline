namespace RasmiOnline.Domain.Enum
{
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Properties;

    public enum StateType : byte
    {
        [LocalizeDescription(nameof(DisplayName.Begin), typeof(DisplayName))]
        Begin = 0,

        [LocalizeDescription(nameof(DisplayName.InProcessing), typeof(DisplayName))]
        InProcessing = 1,

        [LocalizeDescription(nameof(DisplayName.Accepted), typeof(DisplayName))]
        Accepted = 2,

        [LocalizeDescription(nameof(DisplayName.Failed), typeof(DisplayName))]
        Failed = 4,
    }
}
