namespace CharacterModule.Stats.Interfaces
{
    public interface ISubject
    {
        public void Attach(IStatObserver statObserver);
        
        public void Detach(IStatObserver statObserver);
    }
}