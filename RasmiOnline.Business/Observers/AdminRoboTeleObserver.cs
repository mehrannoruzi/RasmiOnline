namespace RasmiOnline.Business.Observers
{
    using Domain.Dto;
    using Domain.Entity;
    using Domain.Enum;
    using Gnu.Framework.Core;
    using Gnu.Framework.EntityFramework.DataAccess;
    using Protocol;

    public class AdminRoboTeleObserver : IObserver
    {
        public void Observe(IUnitOfWork uow, IMessageBusiness messageBusiness, ObserverMessage msg, User user)
        {
            messageBusiness.Insert(new Message
            {
                Content = msg.BotContent.Replace("NAME", user.FullName),
                Receiver = "-1001128896026",
                State = StateType.Begin,
                Type = MessagingType.RoboTele,
                ExtraData = msg.Key,
                ReplyMessageId = msg.MessageId
            });

        }
    }
}
