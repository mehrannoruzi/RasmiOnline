namespace RasmiOnline.MessagingPool.MessagingStrategy
{
    using Domain.Entity;
    using Gnu.Framework.Core;

    public interface IMessagingStrategy
    {
        IActionResponse<bool> Send(Message message);
    }
}
