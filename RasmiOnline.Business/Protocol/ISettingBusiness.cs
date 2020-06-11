namespace RasmiOnline.Business.Protocol
{
    using Domain.Entity;
    using Gnu.Framework.Core;

    public interface ISettingBusiness
    {
        Setting Get();
        IActionResponse<int> Update(Setting model);
    }
}
