namespace RasmiOnline.Business.Protocol
{
    using Domain.Enum;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Collections.Generic;

    public interface IOrderItemBusiness
    {
        IEnumerable<OrderItem> Get(int orderId, OrderItemType? orderItemType = null, string relatedEntities = "none");
        IActionResponse<OrderItem> Delete(int id);
        int GetTotalPrice(int orderId, LangType currentLangType, bool justPricingItems = false);
    }
}