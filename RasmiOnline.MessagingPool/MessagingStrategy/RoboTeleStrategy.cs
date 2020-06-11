namespace RasmiOnline.MessagingPool.MessagingStrategy
{
    using Domain.Entity;
    using Domain.Enum;
    using Gnu.Framework.Core;
    using Gnu.Framework.EntityFramework.DataAccess;
    using System;
    using System.Data.Entity;
    using Telegram.Bot;

    public class RoboTeleStrategy : IMessagingStrategy
    {
        private readonly TelegramBotClient Bot = new TelegramBotClient(AppSettings.TelegramToken);

        #region Constructor
        readonly IUnitOfWork _uow;
        public RoboTeleStrategy(IUnitOfWork uow)
        {
            _uow = uow;
        }
        #endregion

        public IActionResponse<bool> Send(Message message)
        {
            try
            {
                var roboResponse = Bot.SendTextMessageAsync(message.Receiver, message.Content, replyToMessageId: message.ReplyMessageId);
                message.SendStatus = roboResponse.Result.MessageId.ToString();
                message.State = StateType.Accepted;
                _uow.Entry(message).State = EntityState.Modified;
                _uow.SaveChanges();
                return new ActionResponse<bool> { IsSuccessful = true };
            }
            catch (Exception e)
            {
                return new ActionResponse<bool>();
            }
        }
    }
}
