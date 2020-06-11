namespace RasmiOnline.Business.Implement
{
    using System;
    using Gnu.Framework.Core;
    using Protocol;
    using Domain.Entity;
    using Gnu.Framework.EntityFramework.DataAccess;
    using System.Data.Entity;
    using System.Linq;
    using Gnu.Framework.EntityFramework;

    public class ShortLinkBusiness : IShortLinkBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<ShortLink> _shortLink;

        public ShortLinkBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _shortLink = uow.Set<ShortLink>();
        }
        #endregion

        public ShortLink Find(string code) => _shortLink.FirstOrDefault(X => X.Code == code);

        public IActionResponse<int> Insert(ShortLink model)
        {
            model.Code = Randomizer.RandomString(8);
            _shortLink.Add(model);
            var dbRes = _uow.SaveChanges();

            return new ActionResponse<int>()
            {
                IsSuccessful = dbRes.ToSaveChangeResult(),
                Result = model.ShortLinkId
            };
        }
    }
}
