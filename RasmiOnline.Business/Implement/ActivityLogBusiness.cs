namespace RasmiOnline.Business.Implement
{
    using Protocol;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;
    using Properties;

    public class ActivityLogBusiness : IActivityLogBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<ActivityLog> activityLog;

        public ActivityLogBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            activityLog = uow.Set<ActivityLog>();
        }
        #endregion

        /// <summary>
        /// Insert ActivityLog From Filter 
        /// </summary>
        /// <param name="model">ActivityLog model</param>
        /// <returns></returns>
        public IActionResponse<long> Insert(ActivityLog model)
        {
            activityLog.Add(model);
            var result = _uow.SaveChanges();
            return new ActionResponse<long>
            {
                IsSuccessful = result.ToSaveChangeResult(),
                Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model.ActivityLogId
            };
        }
    }
}
