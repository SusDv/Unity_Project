using CharacterModule.Stats.Utility.Enums;
using StatModule.Utility;

namespace CharacterModule.Stats.Interfaces
{
    public interface IStatSubject
    {
        public void AttachStatObserver(IStatObserver statObserver);
        
        public void DetachStatObserver(IStatObserver statObserver);

        public StatInfo GetStatInfo(StatType statType);
    }
}