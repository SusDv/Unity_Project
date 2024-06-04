namespace Utility.ObserverPattern
{
    public interface IStatSubject : ISubject<IStatObserver>
    { }

    public interface ISubject<in T>
    {
        public void AttachObserver(T observer);

        public void DetachObserver(T observer);
    }

    public interface ISubject
    {
        public void AttachObserver(IObserver observer);
        
        public void DetachObserver(IObserver observer);
    }
}