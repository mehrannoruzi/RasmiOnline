namespace RasmiOnline.Business.Implement
{
    using System;
    using Domain.Enum;
    using Domain.Entity;
    using TrezSmsProvider;
    using SharedPreference;
    using System.Data.Entity;
    using Gnu.Framework.Core;
    using Gnu.Framework.Core.Log;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class SmsStrategy : IMessagingStrategy
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly TrezSmsServiceSoapClient _smsAdapter;

        public SmsStrategy(IUnitOfWork uow)
        {
            _uow = uow;
            _smsAdapter = new TrezSmsServiceSoapClient();
        }
        #endregion

        public IActionResponse<bool> Send(Message message)
        {
            var result = new ActionResponse<bool>();
            //try
            //{
           // FileLoger.Info($"Send Sms Message = Receiver:{message.Receiver}, Content:{message.Content}", GlobalVariable.LogPath);
            var sendResult = _smsAdapter.SendMessage("tarjomano1", "11360655", "9830008638000067", message.Content, new ArrayOfString() { message.Receiver }, 1, null);
            if (sendResult[0] > 1000)
            {
                message.State = StateType.Accepted;
                result.IsSuccessful = true;
            }
            message.SendStatus = sendResult[0].ToString();
            _uow.Entry(message).State = EntityState.Modified;
            var saveChange = _uow.SaveChanges();

            result.Result = saveChange.ToSaveChangeResult();
           // FileLoger.Info($"Send Sms Result = IsSuccessful:{result.IsSuccessful}, Result:{sendResult[0]}", GlobalVariable.LogPath);
            //}
            //catch (Exception e) { FileLoger.Error(e, GlobalVariable.LogPath); }
            return result;
        }
    }
}
