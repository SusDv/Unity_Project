using CharacterModule.Stats.Utility.Enums;
using StatModule.Utility;

namespace CharacterModule.Stats.Interfaces
{
    public interface IStatObserver
    {
        public StatType StatType { get; set; }

        public void UpdateValue(StatInfo statInfo);
    }
}