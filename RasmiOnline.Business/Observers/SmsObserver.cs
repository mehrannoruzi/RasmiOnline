namespace RasmiOnline.Business.Observers
{
    using Domain.Dto;
    using Domain.Entity;
    using Domain.Enum;
    using Gnu.Framework.EntityFramework.DataAccess;
    using SharedPreference;
    using System;
    using Gnu.Framework.Core;
    using Protocol;

    public class SmsObserver : IObserver
    {
        public void Observe(IUnitOfWork uow, IMessageBusiness messageBusiness, ObserverMessage msg, User user)
        {
            if (user.MobileNumber == 0)
                return;
            messageBusiness.Insert(new Message
            {
                Content = msg.SmsContent.Replace("NAME", user.FullName),
                Receiver = user.MobileNumber.ToString(),
                State = StateType.Begin,
                Type = MessagingType.Sms,
            });
        }
    }
}
