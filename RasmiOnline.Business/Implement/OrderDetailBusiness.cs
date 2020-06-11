namespace RasmiOnline.Business.Implement
{
    using Protocol;
    using Properties;
    using System.Linq;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;
    using System;

    public class OrderDetailBusiness : IOrderDetailBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<OrderDetail> _orderDetail;

        public OrderDetailBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _orderDetail = uow.Set<OrderDetail>();
        }
        #endregion
        public IEnumerable<OrderDetail> GetAll(int orderId) => _orderDetail.Where(X => X.OrderId == orderId).AsNoTracking().ToList();

        public IActionResponse<int> AddOrUpdate(IEnumerable<OrderDetail> model)
        {
            OrderDetail orderDetail;
            foreach (var item in model)
            {
                if (item.OrderDetailId == 0)
                {
                    item.InsertDateMi = DateTime.Now;
                    item.InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
                    _orderDetail.Add(item);
                }
                else
                {
                    orderDetail = _orderDetail.Find(item.OrderDetailId);
                    if (orderDetail != null)
                    {
                        orderDetail.Title = item.Title;
                        orderDetail.Count = item.Count;
                        orderDetail.Copy = item.Copy;
                        orderDetail.Description = item.Description;
                        _uow.Entry(orderDetail).State = EntityState.Modified;
                    }
                }
            }
            var rep = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                Result = rep,
                Message = rep < 0 ? BusinessMessage.Error : BusinessMessage.Success,
                IsSuccessful = rep.ToSaveChangeResult()
            };
        }
    }
}
