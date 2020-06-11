namespace RasmiOnline.Business.Implement
{
    using System;
    using Domain.Enum;
    using Telegram.Bot;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using System.Configuration;
    using Gnu.Framework.Core.Log;
    using RasmiOnline.SharedPreference;
    using RasmiOnline.Business.Properties;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class RoboTeleStrategy : IMessagingStrategy
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        private readonly TelegramBotClient Bot = new TelegramBotClient(ConfigurationManager.AppSettings["TelegramToken"]);

        public RoboTeleStrategy(IUnitOfWork uow)
        {
            _uow = uow;
        }
        #endregion

        public IActionResponse<bool> Send(Message message)
        {
            var result = new ActionResponse<bool>();
            try
            {
                var roboResponse = Bot.SendTextMessageAsync(message.Receiver, message.Content);
                message.SendStatus = roboResponse.Result.MessageId.ToString();
                message.State = StateType.Accepted;
                _uow.Entry(message).State = EntityState.Modified;
                _uow.SaveChanges();

                result.Result = true;
                result.IsSuccessful = true;
                result.Message = BusinessMessage.Success;
                return result;
            }
            catch (Exception e)
            {
                //FileLoger.Error(e, GlobalVariable.LogPath);

                result.Message = BusinessMessage.Error;
                return result;
            }
        }
    }
}
