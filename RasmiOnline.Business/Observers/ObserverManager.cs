namespace RasmiOnline.Business.Observers
{
    using SharedPreference;
    using Domain.Dto;
    using Gnu.Framework.EntityFramework.DataAccess;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Domain.Entity;
    using Domain.Enum;
    using Protocol;

    public class ObserverManager : IObserverManager
    {
        private readonly IUnitOfWork _uow;
        private readonly IMessageBusiness _messageBusiness;
        public ObserverManager(IUnitOfWork uow, IMessageBusiness messageBusiness)
        {
            _uow = uow;
            _messageBusiness = messageBusiness;
        }
        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer) => _observers.Add(observer);
        public void Detach(IObserver observer) => _observers.Remove(observer);
        public void Notify(ConcreteKey concrete, ObserverMessage msg)
        {
            var user = _uow.Set<User>().Find(msg.UserId);
            if (user == null)
                return;
            var observers = JsonConvert.DeserializeObject<IEnumerable<Concrete>>(File.ReadAllText(GlobalVariable.ObserverConfig));
            foreach (var item in observers)
            {
                if (item.Key == concrete.ToString())
                {
                    foreach (var obs in item.Observers)
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        _observers.Add(assembly.CreateInstance(obs) as IObserver);
                    }
                    foreach (IObserver o in _observers)
                        o.Observe(_uow, _messageBusiness, msg,user);
                    break;
                }
            }
            _uow.SaveChanges();
        }
    }
}