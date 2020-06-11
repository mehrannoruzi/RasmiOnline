namespace RasmiOnline.Business.Implement
{
    using System;
    using System.Linq;
    using Domain.Enum;
    using Domain.Entity;
    using Business.Protocol;
    using System.Data.Entity;
    using Gnu.Framework.Core;
    using Business.Properties;
    using System.Collections.Generic;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class OrderItemBusiness : IOrderItemBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<OrderItem> _orderItem;
        readonly IPricingItemBusiness _pricingItemBusiness;
        public OrderItemBusiness(IUnitOfWork uow, IPricingItemBusiness pricingItemBusiness)
        {
            _uow = uow;
            _orderItem = uow.Set<OrderItem>();
            _pricingItemBusiness = pricingItemBusiness;
        }
        #endregion

        public IEnumerable<OrderItem> Get(int orderId, OrderItemType? orderItemType = null, string relatedEntities = "none")
        {
            var query = _orderItem.AsNoTracking().Where(x => x.OrderId == orderId);
            if (orderItemType != null) query = query.Where(x => x.OrderItemType == orderItemType);
            switch (relatedEntities)
            {
                case "none":
                    break;
                case "all":
                    query = query.Include(x => x.OrderDetails).AsNoTracking();
                    break;
                default:
                    relatedEntities.Split(new char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries).ForEach((entity) =>
                    {
                        query = query.Include(entity).AsNoTracking();
                    });
                    break;
            }
            return query.ToList();
        }

        /// <summary>
        /// Remove an Item An
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResponse<OrderItem> Delete(int id)
        {
            var orderItem = _orderItem.Find(id);
            if (orderItem == null) return new ActionResponse<OrderItem> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
            var officialItem = _orderItem.FirstOrDefault(x => x.OrderId == orderItem.OrderId && x.OrderItemType == OrderItemType.OfficialRecordItem);
            if (orderItem.OrderItemType == OrderItemType.PricingItem && officialItem != null)
            {
                officialItem.Count -= orderItem.Count;
                officialItem.Copy -= orderItem.Copy;
                _uow.Entry(officialItem).State = EntityState.Modified;
            }
            _orderItem.Remove(orderItem);
            var rep = _uow.SaveChanges();
            return new ActionResponse<OrderItem>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Result = orderItem,
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error)
            };
        }

        /// <summary>
        /// Get Total Price For Calcualating Discount Value
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int GetTotalPrice(int orderId, LangType currentLangType, bool justPricingItems = false)
        {
            var q = _orderItem.Where(x => x.OrderId == orderId).AsQueryable();

            if (justPricingItems)
            {
                q = q.Join(_uow.Set<PricingItem>().Where(X => X.IsDiscountable), ordItem => ordItem.PricingItemId,
                  prcItem => prcItem.PricingItemId,
                  (ordItem, prcItem) => ordItem);
            }
            return q.ToList().Sum(x => x.CalculateTotalPrice(currentLangType));
        }
    }
}
