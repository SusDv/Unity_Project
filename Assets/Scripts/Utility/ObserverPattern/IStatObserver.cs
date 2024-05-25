using CharacterModule.Utility;

namespace Utility.ObserverPattern
{
    public interface IStatObserver
    {
        public StatType StatType { get; set; }

        public void UpdateValue(StatInfo statInfo);
    }
}