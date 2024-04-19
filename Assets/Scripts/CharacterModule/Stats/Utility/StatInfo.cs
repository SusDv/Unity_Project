using CharacterModule.Stats.Base;

namespace CharacterModule.Stats.Utility
{
    public class StatInfo
    {
        private StatInfo(Stat stat)
        {
            BaseValue = stat.BaseValue;
            FinalValue = stat.FinalValue;
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