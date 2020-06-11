namespace RasmiOnline.Business.Implement
{
    using System.Linq;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using RasmiOnline.Domain.Entity;
    using RasmiOnline.Business.Protocol;
    using System.Collections.Generic;
    using RasmiOnline.Business.Properties;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;
    using Domain.Enum;
    using System;

    public class OfficeAddressBusiness : IOfficeAddressBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<OfficeAddress> _officeAddress;

        public OfficeAddressBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _officeAddress = _uow.Set<OfficeAddress>();
        }
        #endregion

        public IActionResponse<int> Insert(OfficeAddress model)
        {
            _officeAddress.Add(model);
            var dbRes = _uow.SaveChanges();

            return new ActionResponse<int>()
            {
                IsSuccessful = dbRes.ToSaveChangeResult(),
                Result = model.OfficeAddressId
            };
        }

        public IActionResponse<int> Update(OfficeAddress model)
        {
            var addr = _officeAddress.Find(model.OfficeAddressId);
            if (addr == null) return new ActionResponse<int> { Message = BusinessMessage.RecordNotFound };
            addr.UpdateWith(new
            {
                model.DeliveryName,
                model.FullAddress,
                model.IsActive,
                model.LangType,
                model.Location,
                model.MobileNumber,
                model.Tel,
                model.WorkHour,
                model.UserId
            });
            _uow.Entry(addr).State = EntityState.Modified;
            var res = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = res.ToSaveChangeResult(),
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model.OfficeAddressId
            };
        }

        public IActionResponse<IEnumerable<OfficeAddress>> GetAll()
        {
            var response = new ActionResponse<IEnumerable<OfficeAddress>>();
            response.Result = _officeAddress.OrderByDescending(x => x.OfficeAddressId).ToList();
            if (response.Result != null && response.Result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.RecordNotFound;
            return response;
        }

        public IActionResponse<OfficeAddress> Find(int officeAddressId)
        {
            var response = new ActionResponse<OfficeAddress>();

            response.Result = _officeAddress.FirstOrDefault(x => x.OfficeAddressId == officeAddressId);
            if (response != null)
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.RecordNotFound;

            return response;
        }

        public IActionResponse<bool> ChangeActiveStatus(int officeAddressId)
        {
            var addr = _officeAddress.Find(officeAddressId);
            if (addr == null) return new ActionResponse<bool> { Message = BusinessMessage.RecordNotFound };
            addr.IsActive = !addr.IsActive;
            _uow.Entry(addr).State = EntityState.Modified;
            var res = _uow.SaveChanges();
            return new ActionResponse<bool>
            {
                IsSuccessful = res.ToSaveChangeResult(),
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = res.ToSaveChangeResult()
            };
        }

        public OfficeAddress Get(LangType langType) => _officeAddress.FirstOrDefault(X => X.LangType == langType);
    }
}