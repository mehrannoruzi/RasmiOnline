namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using Domain.Entity;
    public interface IOrderCommentBusiness
    {
        IActionResponse<OrderComment> Add(OrderComment model);
        IActionResponse<OrderComment> Delete(int orderId);
    }
}