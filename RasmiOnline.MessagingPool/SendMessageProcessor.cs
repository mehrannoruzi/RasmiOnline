namespace RasmiOnline.MessagingPool
{
    using Gnu.Framework.Core;
    using Gnu.Framework.Core.Log;
    using Gnu.Framework.EntityFramework.DataAccess;
    using Gnu.Framework.Messaging.Msmq;
    using MessagingStrategy;
    using RasmiOnline.DependencyResolver.Ioc;
    using SharedPreference;
    using System;
    using System.Messaging;

    partial class SendMessageProcessor : WinService
    {
        private readonly IMessagingQueue _queue;
        public override string Name => "RasmiOnline.SendMessageProcessor";

        public SendMessageProcessor()
        {
            InitializeComponent();
            IsUpAndRunning = true;
            _queue = IocInitializer.GetInstance<MSMQ>();
        }


        protected override void OnStop() => IsUpAndRunning = false;

        private void ProcessMessage(object source, ReceiveCompletedEventArgs asyncResult)
        {
            try
            {
                var receiveMessage = (MessageQueue)source;
                var messageQueue = receiveMessage.EndReceive(asyncResult.AsyncResult);

                var message = (Domain.Entity.Message)(messageQueue.Body);
                var result = MessageFactory.GetInstance(message.Type, IocInitializer.GetInstance<IUnitOfWork>()).Send(message);
                receiveMessage.BeginReceive();
            }
            catch (Exception e)
            {
                FileLoger.Error(e, GlobalVariable.LogPath);
            }
        }

        public override void Start()
        {
            try
            {
                //_queue.Receive<Domain.Entity.Message>(GlobalVariable.QueueName, ProcessMessage);
            }
            catch (Exception e)
            {
                FileLoger.Error(e, GlobalVariable.LogPath);
            }
        }

    }
}
