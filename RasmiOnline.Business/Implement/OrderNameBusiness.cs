namespace RasmiOnline.Business.Implement
{
    using Gnu.Framework.Core;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;
    using RasmiOnline.Business.Properties;
    using RasmiOnline.Business.Protocol;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class OrderNameBusiness : IOrderNameBusiness
    {
        #region Constyraucor
        readonly IUnitOfWork _uow;
        readonly IDbSet<OrderName> _orderName;
        public OrderNameBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _orderName = uow.Set<OrderName>();
        }
        #endregion


        public IActionResponse<OrderName> Add(OrderName model)
        {
            _orderName.Add(model);
            var rep = _uow.SaveChanges();
            return new ActionResponse<OrderName> {
                Result = model,
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                IsSuccessful = rep.ToSaveChangeResult()
            };
        }

        /// <summary>
        /// remove order name record by id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IActionResponse<OrderName> Delete(int orderId)
        {
            var item = _orderName.Find(orderId);
            if (item == null) return new ActionResponse<OrderName> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
            _orderName.Remove(item);
            var rep = _uow.SaveChanges();
            return new ActionResponse<OrderName>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = item
            };
        }

    }
}
