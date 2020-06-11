namespace RasmiOnline.Business.Implement
{
    using Protocol;
    using System;
    using System.Collections.Generic;
    using Gnu.Framework.Core;
    using Domain.Entity;
    using Gnu.Framework.EntityFramework.DataAccess;
    using System.Data.Entity;
    using System.Linq;
    using Properties;
    using Gnu.Framework.EntityFramework;

    public class AddressBusiness : IAddressBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<Address> _address;
        public AddressBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _address = uow.Set<Address>();
        }
        #endregion

        public IEnumerable<Address> GetAll(Guid userId) => _address.Where(X => X.UserId == userId && !X.IsDeleted).AsNoTracking().ToList();

        public IActionResponse<int> Insert(Address model)
        {
            _uow.Entry(model).State = EntityState.Added;
            var res = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = res.ToSaveChangeResult(),
                Result = model.AddressId,
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error)
            };
        }

        public IActionResponse<int> Update(Address model)
        {
            var addr = _address.Find(model.AddressId);
            if (addr == null) return new ActionResponse<int> {Message = BusinessMessage.RecordNotFound };
            addr.FullName = model.FullName;
            addr.MobileNumber = model.MobileNumber;
            addr.Tel = model.Tel;
            addr.FullAddress = model.FullAddress;
            addr.Location = model.Location;
            _uow.Entry(addr).State = EntityState.Modified;
            var res = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = res.ToSaveChangeResult(),
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success,BusinessMessage.Error),
                Result = model.AddressId
            };
        }

        public IActionResponse<int> Delete(int addressId)
        {
            var addr = _address.Find(addressId);
            if (addr == null) return new ActionResponse<int> { Message = BusinessMessage.RecordNotFound };
            _uow.Entry(addr).State = EntityState.Deleted;
            var res = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = res.ToSaveChangeResult(),
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = addressId
            };
        }

        public IActionResponse<Address> Find(Guid userId, int addressId)
        {
            var result = _address.Where(X => X.UserId == userId && X.AddressId == addressId).FirstOrDefault();
            if (result == null) return new ActionResponse<Address> { Message = BusinessMessage.RecordNotFound };
            return new ActionResponse<Address> { IsSuccessful = true, Result = result };
        }
    }
}
