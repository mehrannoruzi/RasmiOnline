namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using Domain.Entity;
    using System.Collections.Generic;

    public interface IOrderDetailBusiness
    {
        IEnumerable<OrderDetail> GetAll(int orderId);
        IActionResponse<int> AddOrUpdate(IEnumerable<OrderDetail> model);
    }
}
