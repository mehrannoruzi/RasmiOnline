namespace RasmiOnline.Business.Implement
{
    using System.Linq;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using Domain.Entity;
    using Protocol;
    using System.Collections.Generic;
    using Properties;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;
    using System;

    public class ChannelBusiness : IChannelBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<Channel> _Channel;

        public ChannelBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _Channel = _uow.Set<Channel>();
        }
        #endregion

        public IActionResponse<int> Insert(Channel model)
        {
            _Channel.Add(model);
            var dbRes = _uow.SaveChanges();

            return new ActionResponse<int>()
            {
                IsSuccessful = dbRes.ToSaveChangeResult(),
                Result = model.ChannelId
            };
        }

        public IActionResponse<int> Update(Channel model)
        {
            var addr = _Channel.Find(model.ChannelId);
            if (addr == null) return new ActionResponse<int> { Message = BusinessMessage.RecordNotFound };
            addr.UpdateWith(new
            {
                model.ReferralCode,
                model.UserId
            });
            _uow.Entry(addr).State = EntityState.Modified;
            var res = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = res.ToSaveChangeResult(),
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model.ChannelId
            };
        }

        public IEnumerable<Channel> GetAll() => _Channel.Include(X => X.User).OrderByDescending(X => X.ChannelId).ToList();

        public IEnumerable<Channel> GetAllUserChannel(Guid userId) => _Channel.Where(X=>X.UserId==userId).OrderByDescending(X => X.ChannelId).ToList();

        public IActionResponse<Channel> Find(int ChannelId)
        {
            var response = new ActionResponse<Channel>();

            response.Result = _Channel.FirstOrDefault(x => x.ChannelId == ChannelId);
            if (response != null)
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.RecordNotFound;

            return response;
        }

        public IActionResponse<int> UpdateClick(string refrence)
        {
            var addr = _Channel.FirstOrDefault(X => X.ReferralCode == refrence);
            if (addr == null) return new ActionResponse<int> { Message = BusinessMessage.RecordNotFound };
            addr.UniqueClick += 1;

            var res = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = res.ToSaveChangeResult(),
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
            };
        }
    }
}