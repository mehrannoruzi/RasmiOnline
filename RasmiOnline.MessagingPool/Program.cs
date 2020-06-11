using RasmiOnline.MessagingPool;
using RasmiOnline.MessagingPool.MessagingStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RasmiOnline.MessagingPool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            SmsStrategy sms = new SmsStrategy(null);
            sms.Send(new Domain.Entity.Message
            {
                Content = "test sms rasmi",
                Receiver = "9355706268"
            });
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new SendMessageProcessor()
            //};
            //ServiceBase.Run(ServicesToRun);
        }
    }
}
