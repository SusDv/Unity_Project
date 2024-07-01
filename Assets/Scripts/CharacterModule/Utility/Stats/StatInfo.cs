using CharacterModule.Stats.Base;

namespace CharacterModule.Utility.Stats
{
    public class StatInfo
    {
        private StatInfo(Stat stat)
        {
            BaseValue = stat.BaseValue;
            
            FinalValue = stat.CurrentValue;
            
            MaxValue = stat.MaxValue;
        }

        public float BaseValue { get; private set; }

        public float FinalValue { get; private set; }

        public float MaxValue { get; private set; }

        public static StatInfo GetInstance(Stat stat)
        {
            return new StatInfo(stat);
        }
    }
}