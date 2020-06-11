namespace RasmiOnline.Business.Implement
{
    using System;
    using Protocol;
    using System.IO;
    using System.Web;
    using Properties;
    using Domain.Enum;
    using System.Linq;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;
    using System.Collections.Generic;
    using Domain.Dto;
    using Gnu.Framework.Core.Authentication;
    using Observers;

    public class AttachmentBusiness : IAttachmentBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<Attachment> _attachment;
        readonly Lazy<IObserverManager> _observerManager;

        public AttachmentBusiness(IUnitOfWork uow, Lazy<IObserverManager> observerManager)
        {
            _uow = uow;
            _observerManager = observerManager;
            _attachment = uow.Set<Attachment>();
        }
        #endregion


        public IEnumerable<Attachment> Get(int orderId, AttachmentType type)
            => _attachment.AsNoTracking().Where(x => x.OrderId == orderId && x.AttachmentType == type).ToList();

        /// <summary>
        /// Check & Create An Folder and Returns its Full Path
        /// </summary>
        /// <param name="order"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Tuple<string, string> PrepareFolder(Order order, AttachmentType type)
        {
            var arr = order.InsertDateSh.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var relativePath = $"~/Files/Orders/{arr[0]}/{arr[1]}/{order.OrderId}/{type.ToString()}/";
            var fullPath = HttpContext.Current.Request.MapPath(relativePath);
            if (!Directory.Exists(fullPath)) FileOperation.CreateDirectory(fullPath);
            return new Tuple<string, string>(relativePath, fullPath);
        }

        public Attachment Find(int attachmentId, int orderId) => _attachment.FirstOrDefault(X => X.AttachmentId == attachmentId && X.OrderId == orderId);

        public IActionResponse<int> Insert(Order order, AttachmentType type, List<HttpPostedFileBase> files)
        {
            var result = new ActionResponse<int>();
            var pathes = PrepareFolder(order, type);
            int fileNumber = Directory.GetFiles(pathes.Item2, "*", SearchOption.TopDirectoryOnly).Length;
            foreach (var file in files)
            {
                var ext = Path.GetExtension(file.FileName);
                if (ext == ".exe")
                {
                    return new ActionResponse<int>
                    {
                        IsSuccessful = false,
                        Message = string.Format(BusinessMessage.InvalidFileExt, ".exe")
                    };
                }

                fileNumber++;
                file.SaveAs($"{pathes.Item2}\\{fileNumber.ToString()}{ext}");
                #region Save File Record To Db
                var attachment = new Attachment
                {

                    #region Init Attachment
                    OrderId = order.OrderId,
                    AttachmentType = type,
                    Address = pathes.Item1 + fileNumber.ToString() + ext,
                    OriginalFileName = Path.GetFileName(file.FileName),
                    FileName = fileNumber.ToString(),
                    FileExtention = ext,
                    FileSize = ((int)(file.ContentLength / 1024))
                    #endregion
                };
                _attachment.Add(attachment);
                #endregion 
            }

            var saveResult = _uow.SaveChanges();
            if (saveResult.ToSaveChangeResult())
            {
                _observerManager.Value.Notify(ConcreteKey.Attachment_Add, new ObserverMessage
                {
                    BotContent = string.Format(BusinessMessage.Attachment_Add, order.OrderId, order.OrderStatus.GetLocalizeDescription(), type.GetDescription(), PersianDateTime.Now.ToString(PersianDateTimeFormat.FullDateFullTime)),
                    Key = ConcreteKey.Change_OrderState.ToString(),
                    RecordId = order.OrderId,
                    UserId = (HttpContext.Current.User as ICurrentUserPrincipal).UserId,
                });
            }
            result.Result = saveResult;
            result.IsSuccessful = saveResult.ToSaveChangeResult();
            result.Message = saveResult.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            return result;
        }

        /// <summary>
        /// remove attachemnt by id
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public IActionResponse<Attachment> Delete(int attachmentId)
        {
            var item = _attachment.Find(attachmentId);
            if (item == null) return new ActionResponse<Attachment> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
            _attachment.Remove(item);
            var rep = _uow.SaveChanges();
            return new ActionResponse<Attachment>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = item
            };
        }
    }
}
