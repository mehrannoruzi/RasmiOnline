namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using Domain.Entity;

    public interface IShortLinkBusiness
    {
        IActionResponse<int> Insert(ShortLink model);
        ShortLink Find(string code);
    }
}