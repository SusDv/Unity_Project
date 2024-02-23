using StatModule.Utility;
using StatModule.Utility.Enums;

namespace CharacterModule.Stats.Interfaces
{
    public interface IStatObserver
    {
        public StatType StatType { get; set; }

        public void UpdateValue(StatInfo statInfo);
    }
}