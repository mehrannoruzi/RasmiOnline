namespace RasmiOnline.Business.Observers
{
    using Domain.Dto;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using Gnu.Framework.EntityFramework.DataAccess;
    using Protocol;

    public interface IObserver
    {
        void Observe(IUnitOfWork uow, IMessageBusiness messageBusiness, ObserverMessage msg, User user);
    }
}
