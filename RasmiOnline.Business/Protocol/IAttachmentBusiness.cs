namespace RasmiOnline.Business.Protocol
{
    using System.Web;
    using Domain.Enum;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Collections.Generic;

    public interface IAttachmentBusiness
    {
        Attachment Find(int attachemnetId,int orderId);
        IEnumerable<Attachment> Get(int orderId, AttachmentType type);
        IActionResponse<int> Insert(Order order, AttachmentType type, List<HttpPostedFileBase> files);
        IActionResponse<Attachment> Delete(int attachmentId);
    }
}
