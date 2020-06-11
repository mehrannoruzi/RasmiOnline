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

    public class OrderCommentBusiness : IOrderCommentBusiness
    {
        #region Constyraucor
        readonly IUnitOfWork _uow;
        readonly IDbSet<OrderComment> _OrderComment;
        public OrderCommentBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _OrderComment = uow.Set<OrderComment>();
        }
        #endregion


        public IActionResponse<OrderComment> Add(OrderComment model)
        {
            _OrderComment.Add(model);
            var rep = _uow.SaveChanges();
            return new ActionResponse<OrderComment> {
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
        public IActionResponse<OrderComment> Delete(int orderId)
        {
            var item = _OrderComment.Find(orderId);
            if (item == null) return new ActionResponse<OrderComment> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
            _OrderComment.Remove(item);
            var rep = _uow.SaveChanges();
            return new ActionResponse<OrderComment>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = item
            };
        }

    }
}
