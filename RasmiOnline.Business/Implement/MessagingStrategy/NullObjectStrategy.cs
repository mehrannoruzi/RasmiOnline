namespace RasmiOnline.Business.Implement
{
    using Domain.Entity;
    using Gnu.Framework.Core;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class NullObjectStrategy : IMessagingStrategy
    {
        public NullObjectStrategy(IUnitOfWork uow)
        { }

        public IActionResponse<bool> Send(Message message)
        {
            return new ActionResponse<bool> { IsSuccessful = true };
        }
    }
}
