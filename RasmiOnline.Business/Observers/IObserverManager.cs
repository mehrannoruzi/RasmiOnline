namespace RasmiOnline.Business.Observers
{
    using Domain.Dto;
    using Domain.Enum;

    public interface IObserverManager
    {
        void Notify(ConcreteKey concrete, ObserverMessage msg);
        void Detach(IObserver observer);
        void Attach(IObserver observer);
    }
}