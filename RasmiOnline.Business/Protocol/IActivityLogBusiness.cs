namespace RasmiOnline.Business.Protocol
{
    using Domain.Entity;
    using Gnu.Framework.Core;

    public interface IActivityLogBusiness
    {
        IActionResponse<long> Insert(ActivityLog model);
    }
}
