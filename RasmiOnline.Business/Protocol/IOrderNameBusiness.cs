namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using Domain.Entity;
    public interface IOrderNameBusiness
    {
        IActionResponse<OrderName> Add(OrderName model);
        IActionResponse<OrderName> Delete(int orderId);
    }
}