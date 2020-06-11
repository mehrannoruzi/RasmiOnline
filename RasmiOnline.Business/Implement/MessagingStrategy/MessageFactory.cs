namespace RasmiOnline.Business.Implement
{
    using Domain.Enum;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class MessageFactory
    {
        public static IMessagingStrategy GetInstance(MessagingType type, IUnitOfWork unitOfWork)
        {
            switch (type)
            {
                case MessagingType.RoboTele:
                    return new RoboTeleStrategy(unitOfWork);
                case MessagingType.Sms:
                    return new SmsStrategy(unitOfWork);
                default:
                    return new NullObjectStrategy(unitOfWork);
            }
        }
    }
}
