using CharacterModule.Utility;

namespace Utility.ObserverPattern
{
    public interface IStatSubject
    {
        public void AttachStatObserver(IStatObserver statObserver);
        
        public void DetachStatObserver(IStatObserver statObserver);

        public StatInfo GetStatInfo(StatType statType);
    }
}