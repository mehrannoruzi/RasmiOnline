namespace RasmiOnline.Business.Implement
{
    using Protocol;
    using Properties;
    using System.Linq;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using RasmiOnline.Domain.Enum;
    using System.Collections.Generic;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class MessageBusiness : IMessageBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<Message> _message;

        public MessageBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _message = uow.Set<Message>();
        }
        #endregion

        /// <summary>
        /// Add Message
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResponse<int> Insert(Message model)
        {
            var result = new ActionResponse<int>();

            _message.Add(model);
            var saveResult = _uow.SaveChanges();

            if (saveResult.ToSaveChangeResult())
            {
                var sendResult = MessageFactory.GetInstance(model.Type, _uow).Send(model);
                result.Result = model.MessagingId;
            }

            result.IsSuccessful = saveResult.ToSaveChangeResult();
            result.Message = saveResult.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            return result;
        }
       
        public IActionResponse<IEnumerable<Message>> GetAll(StateType? state = null, MessagingType? messagingType = null)
        {
            var response = new ActionResponse<IEnumerable<Message>>();
            var result = _message.Where(x => (state != null ? x.State == state : true)
                                        && (messagingType != null ? x.Type == messagingType: x.Type == MessagingType.Email || x.Type== MessagingType.Sms))
                                 .OrderByDescending(x => x.MessagingId)
                                 .Select(s => s)
                                 .ToList();
            if (result.IsNotNull().And(result.Any()))
            {
                response.IsSuccessful = true;
                response.Result = result;
            }
            else response.Message = BusinessMessage.RecordNotFound;
            return response;
        }
    }
}