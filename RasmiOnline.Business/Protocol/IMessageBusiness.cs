namespace RasmiOnline.Business.Protocol
{
    using Domain.Entity;
    using Gnu.Framework.Core;
    using Domain.Enum;
    using System.Collections.Generic;

    public interface IMessageBusiness
    {
        IActionResponse<int> Insert(Message model);
        IActionResponse<IEnumerable<Message>> GetAll(StateType? state = null, MessagingType? messagingType = null);
    }
}