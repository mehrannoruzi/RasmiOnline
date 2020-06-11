namespace RasmiOnline.Business.Implement
{
    using Protocol;
    using System.Linq;
    using Domain.Entity;
    using System.Data.Entity;
    using Gnu.Framework.Core;
    using Properties;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;
    using Gnu.Framework.AspNet.Cache;

    public class SettingBusiness : ISettingBusiness
    {
        private readonly string key = $"Settings";

        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<Setting> _setting;
        readonly ICacheProvider _cache;
        public SettingBusiness(IUnitOfWork uow, ICacheProvider cache)
        {
            _uow = uow;
            _setting = uow.Set<Setting>();
            _cache = cache;
        }
        #endregion

        public Setting Get()
        {
            var setting = (Setting)_cache.GetItem(key);
            if (setting != null)
                return setting;
            var findedSetting = _setting.FirstOrDefault();

            _cache.PutItemSliding(key, findedSetting, null, 300);
            return findedSetting;
        }

        public IActionResponse<int> Update(Setting model)
        {
            var response = new ActionResponse<int>();

            var setting = Get();
            if (setting == null)
                response.Message = BusinessMessage.RecordNotFound;
            else
            {
                setting.UpdateWith(new
                {
                    model.DayToDelivery,
                    model.LimitOrderOpenDay,
                    model.OfficialRecordPrice,
                    model.ImportantNotice,
                    model.Tel
                });

                _uow.Entry(setting).State = EntityState.Modified;
                response.Result = _uow.SaveChanges();
                response.IsSuccessful = response.Result.ToSaveChangeResult();
                response.Message = response.Result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);

                if (response.IsSuccessful)
                {
                    _cache.InvalidateItem(key);
                    _cache.PutItemSliding(key, setting, null, 300);
                }
            }
            return response;
        }
    }
}
